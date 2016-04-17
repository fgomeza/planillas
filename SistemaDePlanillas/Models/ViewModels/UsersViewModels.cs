using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public long PrimaryKey { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Display(Name = "Rol")]
        public string Role { get; set; }

        [Display(Name = "Sede")]
        public string Location { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

    }

    public class UserCreateViewModel
    {
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Rol")]
        public long Role { get; set; }

        [Display(Name = "Sede")]
        public long Location { get; set; }
    }
}