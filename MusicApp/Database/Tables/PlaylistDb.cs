using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Database.Tables
{
    internal class PlaylistDb
    {
        public MySqlCon con;

        public PlaylistDb()
        {
            this.con = new MySqlCon();
        }
    }
}
