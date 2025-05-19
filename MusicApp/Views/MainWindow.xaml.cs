using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media;
using MusicApp.Controllers;
using MusicApp.Models;
using Mysqlx.Crud;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;

namespace MusicApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Song> Songs { get; set; }
        private SongController songController;

        public Song SelectedSong { get; set; }
        public MainWindow()
        {
            
            InitializeComponent();
            LoadDemoSongs();

            this.songController = new SongController();

            Songs = new ObservableCollection<Song>(songController.getAll());
            this.DataContext = this;
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Videos MP4 (*.mp4)|*.mp4|Todos (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {

                string audioFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "media/audio");
                Directory.CreateDirectory(audioFolder);

                string originalPath = dlg.FileName;
                string fileName = Path.GetFileName(originalPath);
                string destinationPath = Path.Combine(audioFolder, fileName);

                File.Copy(originalPath, destinationPath, true);

                TagLib.File file = TagLib.File.Create(originalPath);

                this.songController.addSong(file);

                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"media/audio/{fileName}");
                MediaPlayer.Source = new Uri(fullPath, UriKind.Absolute);
                MediaPlayer.Volume = VolumeSlider.Value;
                MediaPlayer.IsMuted = false;
                MediaPlayer.Play();
            }
        }


        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {

            if (Songs == null || Songs.Count == 0 || SelectedSong == null)
                return;

            int currentIndex = Songs.IndexOf(SelectedSong);
            int previousIndex = (currentIndex - 1 + Songs.Count) % Songs.Count;

            SelectedSong = Songs[previousIndex];

            this.ReproduceSong(SelectedSong);

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (Songs == null || Songs.Count == 0 || SelectedSong == null)
                return;

            int currentIndex = Songs.IndexOf(SelectedSong);
            int nextIndex = (currentIndex + 1) % Songs.Count; 

            SelectedSong = Songs[nextIndex]; 

            this.ReproduceSong(SelectedSong);

        }


        private void Timer_Tick(object sender, EventArgs e)
        {
           
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaPlayer.Volume = VolumeSlider.Value;
        }

        private void LoadDemoSongs()
        {
        }

        private void ChangeSong(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Song selectedSong = e.AddedItems[0] as Song;
                if (selectedSong != null)
                {
                    this.ReproduceSong(selectedSong);
                }
            }
        }

        public void ReproduceSong(Song selectedSong)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, selectedSong.FilePath);
            MediaPlayer.Source = new Uri(fullPath, UriKind.Absolute);
            MediaPlayer.Volume = VolumeSlider.Value;
            MediaPlayer.IsMuted = false;
            MediaPlayer.Play();
        }

    }
}