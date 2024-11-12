using System.ComponentModel.DataAnnotations;

namespace Poker.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

}
