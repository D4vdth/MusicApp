using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MusicApp.Database.Tables;
using MusicApp.Models;
using static MusicApp.Database.Tables.CommentsDb;

namespace MusicApp.Services
{
    public class CommentsService
    {

        public CommentsDb dbComments = new CommentsDb();

        public List<Comments> GetCommentsForSong(int songId)
        {
            return dbComments.GetBySongId(songId);
        }

        public void AddComment(int songId, string text, string user = "Anónimo")
        {
            var comment = new Comments
            {
                songId = songId,
                comment = text,
                username = user
            };
            dbComments.Add(comment);
        }
    }
}
