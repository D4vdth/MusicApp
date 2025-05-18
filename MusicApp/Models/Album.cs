using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MusicApp.Models
{
    public class Album
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ArtistId { get; set; } 
        public Artist Artist { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();

    }
}
