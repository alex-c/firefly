using Firefly.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firefly.Repositories.Mocked
{
    public class MockUserRepository : IUserRepository, IReadOnlyUserRepository
    {
        private Dictionary<string, User> Users { get; }

        public MockUserRepository(MockDataProvider dataProvider = null)
        {
            if (dataProvider == null)
            {
                Users = new Dictionary<string, User>();
            }
            else
            {
                Users = dataProvider.Users;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Users.Values;
        }

        public User GetUser(string email)
        {
            return Users.GetValueOrDefault(email);
        }

        public User CreateUser(string email, string name, string password, byte[] salt)
        {
            User user = new User()
            {
                Email = email,
                Name = name,
                Password = password,
                Salt = salt,
            };
            Users.Add(email, user);
            return user;
        }

        public void UpdateUser(User user)
        {
            Users[user.Email] = user;
        }

        public void DeleteUser(string email)
        {
            Users.Remove(email);
        }
    }
}
