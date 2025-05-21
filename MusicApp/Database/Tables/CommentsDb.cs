/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Models;
using MySql.Data.MySqlClient;

namespace MusicApp.Database.Tables
{
    public class CommentsDb
    {

        public void AddComment(Comments comment)
        {
            using var connection = new MySqlConnection(DbConfig.ConnectionString);
            connection.Open();

            string query = "INSERT INTO comments (songId, username, comment, commentDate) VALUES (@SongId, @Username, @Comment, @commentDate)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@SongId", comment.songId);
            command.Parameters.AddWithValue("@Username", comment.username ?? "Anonymous");
            command.Parameters.AddWithValue("@Comment", comment.comment);
            command.Parameters.AddWithValue("@commentDate", comment.timestamp);
            command.ExecuteNonQuery();
        }

        public List<Comments> GetCommentsBySongId(int songId)
        {
            var comments = new List<Comments>();

            using var connection = new MySqlConnection(DbConfig.ConnectionString);
            connection.Open();

            string query = "SELECT * FROM comments WHERE songId = @SongId ORDER BY commentDate DESC";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@SongId", songId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                comments.Add(new Comments
                {
                    Id = Convert.ToInt32(reader["id"]),
                    songId = Convert.ToInt32(reader["songId"]),
                    username = reader["username"].ToString(),
                    comment = reader["comment"].ToString(),
                    timestamp = Convert.ToDateTime(reader["commentDate"])
                });
            }

            return comments;
        }
    }    
}
*/