using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Models;

namespace MusicApp.Models
{
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }      
        public List<Song> Songs { get; set; } = new List<Song>();
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
