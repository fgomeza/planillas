using SistemaDePlanillas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    public class JonnController : Controller
    {
        // GET: Jonn
        public Response Index()
        {
            var x = DBManager.Instance.addUser("Jonn", "JonnCh", "123", 1, 1, "jonn@gmail.com");
           return Responses.SimpleWithData(x.Status,x.Detail);
        }
    }
}