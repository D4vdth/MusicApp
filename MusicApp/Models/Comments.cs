using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int songId { get; set; }
        public string username { get; set; }
        public string comment { get; set; }
        public DateTime timestamp { get; set; }
    }
}
