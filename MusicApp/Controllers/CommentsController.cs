using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    public class CommentsController
    {
        private readonly CommentsService serviceComments = new CommentsService();

        public void AddComment(int songId, string text, string username = "Anonymous")
        {
            var comment = new Comments
            {
                songId = songId,
                username = username,
                comment = text,
                timestamp = DateTime.Now
            };
            serviceComments.AddComment(comment);
        }

        public List<Comments> GetComments(int songId)
        {
            return serviceComments.GetCommentsForSong(songId);
        }
    }
}
