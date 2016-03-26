using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.ViewModels
{
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long Role { get; set; }
        public long Location { get; set; }
    }
}