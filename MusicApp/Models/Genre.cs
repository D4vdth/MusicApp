using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    internal class Genre
    {
        [Key]
        public int GenreId { get; set; }         

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public virtual required ICollection<Album> Albums { get; set; }
        public virtual required ICollection<Song> Songs { get; set; }
    }
}
