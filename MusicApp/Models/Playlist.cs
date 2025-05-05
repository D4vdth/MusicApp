using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    internal class Playlist
    {
        public int Id { get; set; }

        public int cantSong;
        public string username;
        public DateTime createdAt;
        public DateTime updatedAt;

    }
}
