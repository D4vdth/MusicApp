using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Database.Tables
{
    internal class SongDb
    {

        public MySqlCon con;

        public SongDb()
        {
            this.con = new MySqlCon();
        }
    }
}
