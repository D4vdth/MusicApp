using MusicApp.Database.Tables;


namespace MusicApp.Services
{
    internal class ArtistService
    {
        public ArtistDb db;
        public ArtistService()
        {
            this.db = new ArtistDb();
        }


    }
}
