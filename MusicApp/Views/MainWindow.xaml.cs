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

namespace MusicApp
{
    public partial class MainWindow : Window
    {
        private SongController songController;
        public MainWindow()
        {
            InitializeComponent();
            LoadDemoSongs();
            this.songController = new SongController();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Videos MP4 (*.mp4)|*.mp4|Todos (*.*)|*.*"
            };
            if (dlg.ShowDialog() == true)
                MediaPlayer.Source = new Uri(dlg.FileName, UriKind.Absolute);
            MediaPlayer.Volume = VolumeSlider.Value;
            MediaPlayer.IsMuted = false;

            MediaPlayer.Play();
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
           
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            
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
    }
}