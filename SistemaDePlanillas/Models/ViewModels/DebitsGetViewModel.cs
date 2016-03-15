using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.ViewModels
{
    //Si una vista especifica no requiere un modelo puede ser omitido sin problemas

    //Nombre debe ser GroupOperationViewModel
    public class DebitsGetViewModel
    {

        //definir datos de vista
        public string nombre { get; set; }

        //El constructor puede o no recibir el usuario
        public DebitsGetViewModel(User user)
        {
            nombre = user.Name;
            //hacer lo que se quiera
        }
    }
}