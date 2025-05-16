using MusicApp.Database.Tables;
using MusicApp.Models;

namespace MusicApp.Services
{
    internal class SongService
    {
        public SongDb db;
        public SongService()
        {
            this.db = new SongDb();
        }

        public List<Song> getAll()
        {
            return this.db.GetAll();
        }
    }
}
