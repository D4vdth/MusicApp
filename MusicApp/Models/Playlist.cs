using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public ObservableCollection<Song> Songs { get; set; }

    }
}
