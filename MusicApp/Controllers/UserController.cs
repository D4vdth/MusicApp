using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    internal class UserController
    {
        public UserService userService;
        public UserController() { 
            this.userService = new UserService();       
        }
    }
}
