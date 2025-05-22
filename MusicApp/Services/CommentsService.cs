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
        public List<Comments> GetCommentsForSong(int songId) => dbComments.GetCommentsBySongId(songId);

        public void AddComment(Comments comments) => dbComments.AddComment(comments);
    }
}
