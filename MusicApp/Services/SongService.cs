using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Database.Tables;

namespace MusicApp.Services
{
    internal class SongService
    {
        public SongDb db;
        public SongService() { 
            this.db = new SongDb();
        }
    }
}
