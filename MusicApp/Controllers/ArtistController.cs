using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    internal class ArtistController
    {
        public ArtistService artistService;
        public ArtistController()
        {
            this.artistService = new ArtistService();
        }

    }
}
