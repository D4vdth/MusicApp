using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        private string Password { get; set; }

        public string name;
        public int phoneNumber;
        public string email;

    }
}
