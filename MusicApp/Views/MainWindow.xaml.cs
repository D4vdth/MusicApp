using System.Collections.ObjectModel;
using System.Windows;
using MusicApp.Controllers;
using MusicApp.Models;
using Mysqlx.Crud;

namespace MusicApp
{
    public partial class MainWindow : Window
    {
        private SongController songController;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += LoadDemoSongs;
            this.songController = new SongController();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
           
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
            
        }

        private void LoadDemoSongs(object sender, RoutedEventArgs e)
        {
            var songs = this.songController.getAll();
            TracksListBox.ItemsSource = songs;
        }
    }
}