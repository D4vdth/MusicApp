using MusicApp.Controllers;
using System.Windows;
using MusicApp.Models;

namespace MusicApp.Views
{
    public partial class RegisterView : Window
    {
        private readonly UserController _userController;

        public RegisterView()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            var user = new User(username, password);

            _userController.addUser(user);

            this.Close();
        }
    }
}