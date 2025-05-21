using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Services;
using MusicApp.Models;

namespace MusicApp.Controllers
{
    public class CommentsController
    {
        private CommentsService commentService = new CommentsService();

        public List<Comments> GetComments(int songId)
        {
            return commentService.GetCommentsForSong(songId);
        }

        public void AddComment(int songId, string text, string user = "Anónimo")
        {
            commentService.AddComment(songId, text, user);
        }
    }
}
