using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using MusicApp.Models;
using MySql.Data.MySqlClient;

namespace MusicApp.Database.Tables
{
        public class CommentsDb
        {
        public List<Comments> GetCommentsBySongId(int songId)
        {
            var comments = new List<Comments>();

            using (var connection = new MySqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                string query = @"SELECT id, songId, username, comment, commentDate
                                 FROM Comments
                                 WHERE songId = @SongId
                                 ORDER BY commentDate DESC";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SongId", songId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comments.Add(new Comments
                            {
                                Id = (int)reader["id"],
                                songId = (int)reader["songId"],
                                username = reader["username"].ToString(),
                                comment = reader["comment"].ToString(),
                                timestamp = (DateTime)reader["commentDate"]
                            });
                        }
                    }
                }
            }

            return comments;
        }

        public void AddComment(Comments comments)
        {
            using (var connection = new MySqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Comments (songId, username, comment)
                                 VALUES (@SongId, @Username, @Comment)";

                using (var command = new MySqlCommand(query, connection))
                try
                {
                    command.Parameters.AddWithValue("@SongId", comments.songId);
                    command.Parameters.AddWithValue("@Username", comments.username ?? "Anónimo");
                    command.Parameters.AddWithValue("@Comment", comments.comment);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Comment " + comments.comment + " added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                catch (MySqlException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }
    }
}
