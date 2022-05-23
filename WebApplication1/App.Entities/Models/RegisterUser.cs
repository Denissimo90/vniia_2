using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class RegisterUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Не указан логин")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
