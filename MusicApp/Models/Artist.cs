using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Models;

namespace MusicApp.Models
{
    internal class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }        // Propiedades de navegación inversa
        public List<Song> Songs { get; set; } = new List<Song>();
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
