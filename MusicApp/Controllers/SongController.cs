using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    internal class SongController
    {
        public SongService songService;
        public SongController(){
            this.songService = new SongService();
        }
    }
}
