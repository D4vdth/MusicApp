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

        private DispatcherTimer timer;
        private bool isDragging = false;
        private bool isSliderBeingDragged = false;
        private bool isShuffleEnabled = false;
        private bool isRepeatEnabled = false;
        private Random random = new Random();

        public MainWindow()
        {
            
            InitializeComponent();

            PositionSlider.PreviewMouseDown += (s, e) => isSliderBeingDragged = true;
            PositionSlider.PreviewMouseUp += (s, e) =>
            {
                isSliderBeingDragged = false;
                // Adelanta o retrocede el audio al soltar el mouse
                MediaPlayer.Position = TimeSpan.FromSeconds(PositionSlider.Value);
            };

            this.songController = new SongController();

            Songs = new ObservableCollection<Song>(songController.getAll());
            this.DataContext = this;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
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
                Songs.Clear();
                foreach (var song in songController.getAll())
                {
                    Songs.Add(song);
                }

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
            if (isShuffleEnabled)
            {
                if (Songs.Count > 0)
                {
                    int randomIndex = random.Next(Songs.Count);
                    SelectedSong = Songs[randomIndex];
                    ReproduceSong(SelectedSong);
                }
            }
            if (Songs == null || Songs.Count == 0 || SelectedSong == null)
                return;

            int currentIndex = Songs.IndexOf(SelectedSong);
            int nextIndex = (currentIndex + 1) % Songs.Count; 

            SelectedSong = Songs[nextIndex]; 

            this.ReproduceSong(SelectedSong);

        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging && MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                PositionSlider.Value = MediaPlayer.Position.TotalSeconds;
            }
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Solo cambiar posición si el usuario NO está arrastrando
            if (!isSliderBeingDragged)
            {
                MediaPlayer.Position = TimeSpan.FromSeconds(PositionSlider.Value);
            }
        }

        private void PositionSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void PositionSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            isDragging = false;
            MediaPlayer.Position = TimeSpan.FromSeconds(PositionSlider.Value);
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaPlayer.Volume = VolumeSlider.Value;
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

            MediaPlayer.MediaOpened += (s, e) =>
            {
                // ayuda a establecer el rango máximo del slider según la duración de la cancion
                PositionSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                // ajusta para pequeños y grandes movimientos del slider
                PositionSlider.SmallChange = 1; // paso pequeño: 1 segundo
                // actualiza la posición del slider con un temporizador
                timer.Start();
            };

            MediaPlayer.Play();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(query))
            {
                // Mostrar todas las canciones
                Songs.Clear();
                foreach (var song in songController.getAll())
                {
                    Songs.Add(song);
                }
            }
            else
            {
                var filteredSongs = songController.Search(query);
                Songs.Clear();
                foreach (var song in filteredSongs)
                {
                    Songs.Add(song);
                }
            }
        }

        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (isRepeatEnabled)
            {
                MediaPlayer.Position = TimeSpan.Zero;
                MediaPlayer.Play();
            }
            else if (isShuffleEnabled)
            {
                if (Songs.Count > 0)
                {
                    int randomIndex = random.Next(Songs.Count);
                    SelectedSong = Songs[randomIndex];
                    ReproduceSong(SelectedSong);
                }
            }
            else
            {
                NextButton_Click(null, null);
            }
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            isShuffleEnabled = !isShuffleEnabled;
            ShuffleButton.Background = isShuffleEnabled ? Brushes.LightGreen : Brushes.Transparent;
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            isRepeatEnabled = !isRepeatEnabled;
            RepeatButton.Background = isRepeatEnabled ? Brushes.LightGreen : Brushes.Transparent;
        }

    }
}