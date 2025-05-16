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

        public List<Song> getAll()
        {
            return this.songService.getAll();
        }
    }
}
