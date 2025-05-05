using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    internal class ArtistController
    {
        public ArtistService artistService;
        public ArtistController() {
            this.artistService = new ArtistService();
        }
    }
}
