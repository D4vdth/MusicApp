using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    internal class SongController
    {
        public SongService songService;
        public SongController()
        {
            this.songService = new SongService();
        }

        public void addSong(TagLib.File file)
        {
            this.songService.addSong(file);
        }

        public List<Song> getAll()
        {
            return this.songService.getAll();
        }

        public List<Song> Search(string query)
        {
            return this.songService.Search(query);
        }

        public void UpdateRating(string songId, int rating)
        {
            this.songService.UpdateRating(songId, rating);
        }
    }
}
