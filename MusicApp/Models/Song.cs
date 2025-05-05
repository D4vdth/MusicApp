using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    internal class Song
    {
        public int Id { get; set; }

        public string title;
        public string artist;
        public string letter;
        public string about;
        public DateTime releaseDate;

    }
}
