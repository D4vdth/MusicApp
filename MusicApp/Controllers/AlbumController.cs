using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    internal class AlbumController
    {
        public AlbumService albumService;
        public AlbumController() {
            this.albumService = new AlbumService();
        }
    }
}
