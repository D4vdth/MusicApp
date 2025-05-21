using MusicApp.Database.Tables;
using MusicApp.Models;
using System.IO;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace MusicApp.Services
{
    internal class UserService
    {
        public UserDb db;
        public UserService()
        {
            this.db = new UserDb();
        }

        public void add(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            this.db.add(user);
        }

        public User get(string username)
        {
            if (string.IsNullOrEmpty(username)) { 
                throw new ArgumentNullException("not username");
            }
            return this.db.GetUserByUsername(username);
        }

        public Boolean validateUser(string username, string password)
        {
            User user = this.db.GetUserByUsername(username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return true;
            }


            return false;

        }

    }
}
