using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Database.Tables
{
    internal class AlbumDb
    {
        public MySqlCon con;

        public AlbumDb()
        {
            this.con = new MySqlCon();
        }
    }
}
