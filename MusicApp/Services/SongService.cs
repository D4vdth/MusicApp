using MusicApp.Database.Tables;
using MusicApp.Models;
using System.IO;

namespace MusicApp.Services
{
    internal class SongService
    {
        public SongDb db;
        public SongService()
        {
            this.db = new SongDb();
        }

        public void addSong(TagLib.File file)
        {

            string fileName = Path.GetFileName(file.Name);
            Song song = new Song();

            song.Title = file.Tag.Title ?? fileName; 
            song.Duration = file.Properties.Duration;
            song.FilePath = $"media/audio/{fileName}";

            this.db.add(song);
        }

        public List<Song> getAll()
        {
            return this.db.GetAll();
        }
    }
}
