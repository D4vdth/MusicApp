using MusicApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MusicApp.Database.Tables
{
    internal class SongDb
    {
        public List<Song> GetAll()
        {
            var songs = new List<Song>();
            string query = @"
                SELECT
                    s.Id, s.Title, s.ArtistId, s.AlbumId, s.Duration, s.FilePath, s.AlbumCoverPath, 
                    a.Id AS ArtistId, a.Name AS ArtistName,
                    al.Id AS AlbumId, al.Name AS AlbumTitle, al.ArtistId AS AlbumArtistId
                FROM Song s
                INNER JOIN Artist a ON s.ArtistId = a.Id
                INNER JOIN Album al ON s.AlbumId = al.Id";

            using (var connection = new MySqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var artist = new Artist
                        {
                            Id = reader.GetInt32("ArtistId").ToString(),
                            Name = reader.GetString("ArtistName")
                        };

                        var album = new Album
                        {
                            Id = reader.GetInt32("AlbumId").ToString(),
                            Title = reader.GetString("AlbumTitle"),
                            ArtistId = reader.GetInt32("AlbumArtistId").ToString(),
                            Artist = artist
                        };

                        var song = new Song
                        {
                            Id = reader.GetInt32("Id").ToString(),
                            Title = reader.GetString("Title"),
                            ArtistId = reader.GetInt32("ArtistId").ToString(),
                            AlbumId = reader.GetInt32("AlbumId").ToString(),
                            Duration = reader.GetTimeSpan("Duration"),
                            FilePath = reader.IsDBNull(reader.GetOrdinal("FilePath")) ? null : reader.GetString("FilePath"),
                            AlbumCoverPath = reader.IsDBNull(reader.GetOrdinal("AlbumCoverPath")) ? null : reader.GetString("AlbumCoverPath"), // Corregido el nombre de la columna
                            Artist = artist,
                            Album = album
                        };

                        songs.Add(song);
                    }
                }
            }
            return songs;
        }

        public void add(Song song) 
        {
            string query = @"
                INSERT INTO Song (Title, ArtistId, AlbumId, Duration, FilePath)
                VALUES (@Title, @ArtistId, @AlbumId, @Duration, @FilePath)";
            using (var connection = new MySqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Title", song.Title);
                        command.Parameters.AddWithValue("@ArtistId", 1);
                        command.Parameters.AddWithValue("@AlbumId", 1);
                        command.Parameters.AddWithValue("@Duration", song.Duration);
                        command.Parameters.AddWithValue("@FilePath", song.FilePath);
                        command.ExecuteNonQuery();

                        Console.WriteLine("Cancion: " + song.Title + " guardada con exito");
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }

        }
    }
}
