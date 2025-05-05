using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Database.Tables;

namespace MusicApp.Services
{
    internal class PlaylistService
    {
        public PlaylistDb db;
        public PlaylistService(){
            this.db = new PlaylistDb();
        }
    }
}
