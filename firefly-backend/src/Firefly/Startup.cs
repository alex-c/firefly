using Firefly.Repositories;
using Firefly.Repositories.Mocked;
using Firefly.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Text;

namespace Firefly
{
    public class Startup
    {
        /// <summary>
        /// CORS policy name for local development.
        /// </summary>
        private readonly string LOCAL_DEVELOPMENT_CORS_POLICY = "localDevelopmentCorsPolicy";

        /// <summary>
        /// The hosting environment information.
        /// </summary>
        private IWebHostEnvironment Environment { get; }

        /// <summary>
        /// The app configuration.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Injects modules necessary to app and services configuration.
        /// </summary>
        /// <param name="configuration">App configuration.</param>
        /// <param name="environemnt">Hosting environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environemnt)
        {
            Configuration = configuration;
            Environment = environemnt;
        }

        /// <summary>
        /// Configures app services.
        /// </summary>
        /// <param name="services">Service collection to extend.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure CORS
            services.AddCors(options =>
            {
                if (Environment.IsDevelopment())
                {
                    options.AddPolicy(LOCAL_DEVELOPMENT_CORS_POLICY, builder =>
                    {
                        builder.WithOrigins("http://localhost:8080")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                    });
                }
            });

            // Configure JWT-based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetValue<string>("Jwt:Issuer"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Jwt:Secret")))
                    };
                });

            services.AddAuthorization();

            // Set up repositories
            if (Configuration.GetValue<bool>("Mocking:UseMockDataPersistence"))
            {
                if (Configuration.GetValue<bool>("Mocking:SeedWithMockDataOnStartup"))
                {
                    services.AddSingleton<MockDataProvider>();
                }
                services.AddSingleton<IUserRepository, MockUserRepository>();
                services.AddSingleton<IReadOnlyUserRepository, MockUserRepository>();
            }
            else
            {
                // TODO: implement repositories for PostgreSQL persistence
                throw new NotImplementedException("PostgreSQL-based persistence hasn't been implemented yet.");
            }

            services.AddSingleton<PasswordHashingService>();
            services.AddSingleton<AuthService>();
            services.AddSingleton<UserService>();

            services.AddControllers();
        }

        /// <summary>
        /// Configures the app.
        /// </summary>
        /// <param name="app">Application builder to configure the app through.</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseCors(LOCAL_DEVELOPMENT_CORS_POLICY);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });


        }
    }
}
