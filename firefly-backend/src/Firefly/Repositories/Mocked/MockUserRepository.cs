using Firefly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<User> SearchUsersByName(string partialName)
        {
            return Users.Values.Where(u => u.Name.ToLowerInvariant().Contains(partialName.ToLowerInvariant()));
        }

        public User GetUser(string id)
        {
            return Users.GetValueOrDefault(id);
        }

        public User CreateUser(string id, string name, string password, byte[] salt, bool isAdmin)
        {
            User user = new User()
            {
                Id = id,
                Name = name,
                Password = password,
                Salt = salt,
                IsAdmin = isAdmin,
            };
            Users.Add(id, user);
            return user;
        }

        public void UpdateUser(User user)
        {
            Users[user.Id] = user;
        }

        public void DeleteUser(string email)
        {
            Users.Remove(email);
        }
    }
}
