using System.ComponentModel.DataAnnotations;


namespace MusicApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public User(string username, string password) {
            this.Username = username;
            this.Password = password;
        }
    }
}
