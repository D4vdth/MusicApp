using MusicApp.Database.Tables;
using MusicApp.Models;
using System.IO;

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
            this.db.add(user);
        }

        public User get(string username)
        {
            if (string.IsNullOrEmpty(username)) { 
                throw new ArgumentNullException("not username");
            }
            return this.db.GetUserByUsername(username);
        }

    }
}
