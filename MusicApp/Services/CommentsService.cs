/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Database.Tables;
using MusicApp.Models;

namespace MusicApp.Services
{
    public class CommentsService
    {
        private readonly CommentsDb dbComments = new CommentsDb();

        public void AddComment(Comments comments)
        {
            dbComments.AddComment(comments);
        }

        public List<Comments> GetCommentsForSong(int songId)
        {
            return dbComments.GetCommentsBySongId(songId);
        }
    }
}
*/