using MusicApp.Database.Tables;
using MusicApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicApp.Controllers
{
    internal class PlaylistController
    {
        private readonly PlaylistDb db = new PlaylistDb();


        public ObservableCollection<Playlist> GetAll()
        {
            var list = new ObservableCollection<Playlist>(db.GetAll());
           
            foreach (var pl in list)
            {
                pl.Songs = new ObservableCollection<Song>(
                    db.GetSongsByPlaylist(pl.Id.ToString()));
            }
            return list;
        }

       
        public void Create(string name) => db.Create(name);

      
        public void AddSong(string playlistId, string songId)
            => db.AddSong(playlistId, songId);

  
        public ObservableCollection<Song> GetSongs(string playlistId)
            => new ObservableCollection<Song>(
                   db.GetSongsByPlaylist(playlistId));
    }
}
