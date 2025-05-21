using MusicApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MusicApp.Database.Tables
{
    internal class PlaylistDb
    {
        // 1) Lee todas las playlists (sólo Id y Name)
        public List<Playlist> GetAll()
        {
            var list = new List<Playlist>();
            const string sql = "SELECT Id, Name FROM Playlist";

            using var conn = new MySqlConnection(DbConfig.ConnectionString);
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new Playlist
                {
                    Id = rdr.GetGuid("Id"),
                    Name = rdr.GetString("Name")
                });
            }
            return list;
        }

        // 2) Inserta una nueva playlist
        public void Create(string name)
        {
            var id = Guid.NewGuid().ToString();
            const string sql = "INSERT INTO Playlist (Id, Name) VALUES (@Id, @Name)";

            using var conn = new MySqlConnection(DbConfig.ConnectionString);
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.ExecuteNonQuery();
        }

        // 3) Inserta la relación Playlist–Song
        public void AddSong(string playlistId, string songId)
        {
            const string sql = "INSERT IGNORE INTO PlaylistSong (PlaylistId, SongId) VALUES (@P, @S)";

            using var conn = new MySqlConnection(DbConfig.ConnectionString);
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@P", playlistId);
            cmd.Parameters.AddWithValue("@S", songId);
            cmd.ExecuteNonQuery();
        }

        // 4) Opcional: obtiene las canciones de una playlist
        public List<Song> GetSongsByPlaylist(string playlistId)
        {
            var songs = new List<Song>();
            const string sql = @"
                SELECT s.Id, s.Title, s.ArtistId, s.AlbumId, s.Duration, s.FilePath, s.AlbumCoverPath
                FROM Song s
                INNER JOIN PlaylistSong ps ON ps.SongId = s.Id
                WHERE ps.PlaylistId = @P";

            using var conn = new MySqlConnection(DbConfig.ConnectionString);
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@P", playlistId);

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                songs.Add(new Song
                {
                    Id = rdr.GetInt32("Id").ToString(),
                    Title = rdr.GetString("Title"),
                    ArtistId = rdr.GetInt32("ArtistId").ToString(),
                    AlbumId = rdr.GetInt32("AlbumId").ToString(),
                    Duration = rdr.GetTimeSpan("Duration"),
                    FilePath = rdr.IsDBNull(rdr.GetOrdinal("FilePath"))
                                        ? null
                                        : rdr.GetString("FilePath"),
                    AlbumCoverPath = rdr.IsDBNull(rdr.GetOrdinal("AlbumCoverPath"))
                                        ? null
                                        : rdr.GetString("AlbumCoverPath")
                });
            }
            return songs;
        }
    }
}
