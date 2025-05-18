using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace MusicApp.Database.Tables
{
    internal class AlbumDb
    {  
        public List<Album> GetAll()
        {

            var listAlbum = new List<Album>();

            using (var con = new MySqlConnection(DbConfig.ConnectionString))
            {
                con.Open();

                using(var command = new MySqlCommand("SELECT * FROM Album", con))

                using(var rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        listAlbum.Add(new Album
                        {
                            Id = rdr.GetString("Id"),
                            Title = rdr.GetString("Title"),
                            ArtistId = rdr.GetString("ArtistId"),
                            ReleaseDate = rdr.GetDateTime("ReleaseDate"),
                        });
                    }
                }
            }
            return listAlbum;
        }
    }
}
