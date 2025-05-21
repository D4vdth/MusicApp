using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    internal class UserController
    {
        public UserService userService;
        public UserController()
        {
            this.userService = new UserService();
        }

        public void addUser(User user)
        {
            this.userService.add(user);
        }

        public User get(string username)
        {
            return this.userService.get(username);
        }
    }
}
