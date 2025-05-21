using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Services;
using MusicApp.Models;
using System.Xml.Linq;

namespace MusicApp.Controllers
{
    public class CommentsController
    {
        private CommentsService commentService = new CommentsService();
        public List<Comments> GetComments(int songId)
        {
            return commentService.GetCommentsForSong(songId);
        }

        public void AddComment(int songId, string text, string username = "Anónimo")
        {
            var comment = new Comments
            {
                songId = songId,
                username = username,
                comment = text,
                timestamp = DateTime.Now
            };

            commentService.AddComment(comment);
        }
    }
}
