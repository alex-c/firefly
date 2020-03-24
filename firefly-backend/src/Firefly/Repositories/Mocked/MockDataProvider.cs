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
            GenerateUser("alex", "Alexandre Charoy", true);
            GenerateUser("anna", "Anna Annamann");
            GenerateUser("jinx", "Jinx LoL");
        }

        private void GenerateUser(string id, string name, bool isAdmin = false)
        {
            (string hash, byte[] salt) = PasswordHashingService.HashAndSaltPassword("test");
            User user = new User()
            {
                Id = id,
                Password = hash,
                Salt = salt,
                Name = name,
                IsAdmin = isAdmin
            };
            Users.Add(user.Id, user);
        }

        #endregion
    }
}
