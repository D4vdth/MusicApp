using MusicApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicApp.Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly UserController userController;

        public Login()
        {
            InitializeComponent();
            userController = new UserController();

        }
        
        public void LoginButton_Click(object sender, RoutedEventArgs e) 
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (!userController.validateUser(username, password))
            {
                MessageBox.Show("Credenciales invalidas ❌");
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }


        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
        }
    }
}
