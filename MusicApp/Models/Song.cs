using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistId { get; set; }
        public string AlbumId { get; set; }
        public TimeSpan Duration { get; set; }
        public string FilePath { get; set; }
        public string AlbumCoverPath { get; set; }

        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public int Rating { get; set; }


    }
}
