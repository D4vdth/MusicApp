
using MusicApp.Database.Tables;


namespace MusicApp.Services
{
    internal class AlbumService
    {
        public AlbumDb db;
        public AlbumService() {
            this.db = new AlbumDb();
        }
    }
}
