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
using MusicApp.Views;
using Microsoft.VisualBasic;

namespace MusicApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Song> Songs { get; set; }
        private SongController songController;
        private CommentsController commentsController = new CommentsController();
        public Song SelectedSong { get; set; }

        private DispatcherTimer timer;
        private bool isDragging = false;
        private bool isSliderBeingDragged = false;
        private bool isShuffleEnabled = false;
        private bool isRepeatEnabled = false;
        private Random random = new Random();
        private Song currentSong;

        private readonly PlaylistController playlistController = new PlaylistController();
        public ObservableCollection<Playlist> Playlists { get; set; } = new();

        private Playlist _selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                _selectedPlaylist = value;

                if (_selectedPlaylist != null)
                {
                    // Llama a un método que use GetSongsByPlaylist
                    LoadSongsFromPlaylist(_selectedPlaylist.Id.ToString());
                }
            }
        }

        public ObservableCollection<Song> SongsInPlaylist { get; set; } = new();


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

            Playlists = playlistController.GetAll();
            PlaylistListBox.ItemsSource = Playlists;
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
            currentSong = selectedSong;
            LoadSong(selectedSong);

            MediaPlayer.MediaOpened += (s, e) =>
            {
                // ayuda a establecer el rango máximo del slider según la duración de la cancion
                //PositionSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                // ajusta para pequeños y grandes movimientos del slider
                PositionSlider.SmallChange = 1; // paso pequeño: 1 segundo
                // actualiza la posición del slider con un temporizador
                timer.Start();
            };

            MediaPlayer.Play();
            LoadComments(selectedSong.Id);
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

        public void LoadSong(Song song)
        {
            currentSong = song;
            UpdateStarDisplay(song.Rating);
        }

        private void UpdateStarDisplay(int rating)
        {
            foreach (Button starButton in StarPanel.Children)
            {
                int starNumber = int.Parse(starButton.Tag.ToString());
                starButton.Content = starNumber <= rating ? "★" : "☆";
            }
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            Button clickedStar = sender as Button;
            if (clickedStar == null)
                return;

            int rating = int.Parse(clickedStar.Tag.ToString());

            if (currentSong == null)
            {
                MessageBox.Show("No hay canción cargada");
                return;
            }
            currentSong.Rating = rating;
            songController.UpdateRating(currentSong.Id, rating);
            UpdateStarDisplay(rating);
        }


        private void OpenRegisterView_Click(object sender, RoutedEventArgs e)
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
        }

        /*  private void LoadComments(int songId)
          {
              var comments = commentsController.GetComments(songId);
              CommentList.Items.Clear();
              foreach (var c in comments)
              {
                  CommentList.Items.Add($"{c.username}: {c.comment} ({c.timestamp:t})");
              }
          }
          private void AddComment_Click(object sender, RoutedEventArgs e)
          {
              if (SelectedSong != null && !string.IsNullOrWhiteSpace(NewCommentBox.Text))
              {
                  commentsController.AddComment(SelectedSong.Id, NewCommentBox.Text.Trim());
                  NewCommentBox.Clear();
                  LoadComments(SelectedSong.Id);
              }
          }
          private void NewCommentBox_GotFocus(object sender, RoutedEventArgs e)
          {
              if (NewCommentBox.Text == "Escribe un comentario...")
              {
                  NewCommentBox.Text = "";
                  NewCommentBox.Foreground = Brushes.Black;
              }
          }
          private void NewCommentBox_LostFocus(object sender, RoutedEventArgs e)
          {
              if (string.IsNullOrWhiteSpace(NewCommentBox.Text))
              {
                  NewCommentBox.Text = "Escribe un comentario...";
                  NewCommentBox.Foreground = Brushes.Gray;
              }
          }*/
        private void CreatePlaylist_Click(object sender, RoutedEventArgs e)
        {

            string playlistName = Interaction.InputBox("Ingrese el nombre de la nueva playlist:", "Crear Playlist", "");
            if (!string.IsNullOrWhiteSpace(playlistName))
            { 
                if (string.IsNullOrWhiteSpace(playlistName))
                {
                    MessageBox.Show("Debes ingresar un nombre válido.");
                    return;
                }

                // Crear la playlist en la base de datos
                playlistController.Create(playlistName);

                // Recargar todas las playlists
                Playlists = playlistController.GetAll();
                PlaylistListBox.ItemsSource = Playlists;

                // Buscar la nueva playlist por nombre
                var newPlaylist = Playlists.FirstOrDefault(p => p.Name == playlistName);
                if (newPlaylist != null && SelectedSong != null)
                {
                    playlistController.AddSong(newPlaylist.Id.ToString(), SelectedSong.Id);
                    newPlaylist.Songs.Add(SelectedSong); // Reflejar en memoria
                    MessageBox.Show($"La canción '{SelectedSong.Title}' fue añadida a la playlist '{playlistName}'.");
                }
            }
        }

        private void AddPlayListButton_Click(object sender, RoutedEventArgs e)
        {
            var listas = playlistController.GetAll();
            string playlistId;

            if (listas.Count == 0)
            {
                playlistId = CreateNewPlaylist();
                if (playlistId == null) return;
            }
            else
            {
                playlistId = SelectOrCreatePlaylist(listas);
                if (playlistId == null) return;
            }
            if (SelectedSong == null)
            {
                MessageBox.Show("Selecciona primero una canción.", "Atención",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            playlistController.AddSong(playlistId, SelectedSong.Id);

            RefreshPlaylistsInUI();

            MessageBox.Show($"\"{SelectedSong.Title}\" añadida a la playlist.",
                            "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private string CreateNewPlaylist()
        {
            string nombre = Interaction.InputBox(
                "Escribe el nombre de la nueva playlist:",
                "Crear Playlist",
                "Mi Playlist");

            if (string.IsNullOrWhiteSpace(nombre))
                return null;

            playlistController.Create(nombre);
            var todas = playlistController.GetAll();
            return todas.Last().Id.ToString();
        }
        private string SelectOrCreatePlaylist(IList<Playlist> listas)
        {
            var texto = string.Join("\n", listas.Select((p, i) => $"{i + 1}. {p.Name}"));
            texto += "\n\nEscribe el número de playlist o N para nueva:";

            string resp = Interaction.InputBox(texto, "Seleccionar Playlist", "1");
            if (string.IsNullOrWhiteSpace(resp)) return null;

            if (resp.Trim().ToUpper() == "N")
                return CreateNewPlaylist();

            if (int.TryParse(resp, out int idx) && idx >= 1 && idx <= listas.Count)
                return listas[idx - 1].Id.ToString();

            MessageBox.Show("Entrada no válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

       
        private void RefreshPlaylistsInUI()
        {
            Playlists.Clear();
            foreach (var p in playlistController.GetAll())
            {
             
                Playlists.Add(p);
            }
            PlaylistListBox.Items.Refresh();
        }

        private void LoadSongsFromPlaylist(string playlistId)
        {
            var songs = playlistController.GetSongs(playlistId); 
            SongsInPlaylist.Clear();

            foreach (var song in songs)
            {
                SongsInPlaylist.Add(song);
            }
        }



        private void LoadComments(int songId)
        {
            var comments = commentsController.GetComments(songId);

            CommentList.Items.Clear();

            foreach (var c in comments)
            {
                CommentList.Items.Add($"{c.username}: {c.comment} ({c.timestamp:t})");
            }
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSong != null && !string.IsNullOrWhiteSpace(NewCommentBox.Text)
                && NewCommentBox.Text != "Escribe un comentario...")
            {
                    commentsController.AddComment(SelectedSong.Id, NewCommentBox.Text.Trim());
                    NewCommentBox.Clear();
                    LoadComments(SelectedSong.Id);
            }
        }
        private void NewCommentBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NewCommentBox.Text == "Escribe un comentario...")
            {
                NewCommentBox.Text = "";
                NewCommentBox.Foreground = Brushes.Black;
            }
        }
        private void NewCommentBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewCommentBox.Text))
            {
                NewCommentBox.Text = "Escribe un comentario...";
                NewCommentBox.Foreground = Brushes.Gray;
            }
        }
    }
}