using Firefly.Models;
using Firefly.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firefly.Repositories.Mocked
{
    public class MockDataProvider
    {
        private PasswordHashingService PasswordHashingService { get; }

        public Dictionary<string, User> Users { get; }

        public MockDataProvider(PasswordHashingService passwordHashingService)
        {
            PasswordHashingService = passwordHashingService;

            Users = new Dictionary<string, User>();

            GenerateUsers();
        }

        #region Users

        private void GenerateUsers()
        {
            GenerateUser("alex", "Alexandre Charoy");
            GenerateUser("anna", "Anna Annamann");
            GenerateUser("jinx", "Jinx LoL");
        }

        private void GenerateUser(string email, string name)
        {
            (string hash, byte[] salt) = PasswordHashingService.HashAndSaltPassword("test");
            User user = new User()
            {
                Email = email,
                Password = hash,
                Salt = salt,
                Name = name,
            };
            Users.Add(user.Email, user);
        }

        #endregion
    }
}
