using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name="Usuario")]
        public string Username { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

    }
}