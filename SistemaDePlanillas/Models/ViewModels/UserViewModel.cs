using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}