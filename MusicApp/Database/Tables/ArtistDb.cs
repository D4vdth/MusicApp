    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Database.Tables
{
    internal class ArtistDb
    {
        public MySqlCon con;

        public ArtistDb()
        {
            this.con = new MySqlCon();
        }
    }
}
