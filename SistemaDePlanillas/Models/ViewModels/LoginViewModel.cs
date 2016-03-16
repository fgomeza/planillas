using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Debe entrar un nombre de usuario")]
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Debe entrar una contraseña")]
        public string Password { get; set; }

        [Display(Name ="Recordar usuario")]
        public bool RememberMe { get; set; }
    }
}