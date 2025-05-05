using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Database;
using MusicApp.Models;

namespace MusicApp.Database.Tables
{
    internal class UserDb
    {
        public MySqlCon con;

        public UserDb()
        {
            this.con = new MySqlCon();
        }
    }
}
