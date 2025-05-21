using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MusicApp.Models;
using MySql.Data.MySqlClient;

namespace MusicApp.Database.Tables
{
        public class CommentsDb
        {
            private static List<Comments> comments = new List<Comments>();

            public List<Comments> GetBySongId(int songId)
            {
                return comments.Where(c => c.songId == songId).OrderByDescending(c => c.Timestamp).ToList();
            }

            public void Add(Comments comment)
            {
                comment.Timestamp = DateTime.Now;
                comments.Add(comment);
            }
        }
}
