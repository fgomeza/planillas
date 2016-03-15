using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.ViewModels
{
    public class DebitsGetViewModel
    {
        public string nombre { get; set; }
        public DebitsGetViewModel(User user)
        {
            nombre = user.Name;
        }
    }
}