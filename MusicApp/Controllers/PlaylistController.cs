using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    internal class PlaylistController
    {
        public PlaylistService playlistService;
        public PlaylistController() {
            this.playlistService = new PlaylistService();
        }
    }
}
