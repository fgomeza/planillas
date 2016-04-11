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
        public string Index()
        {
            var x = DBManager.Instance.deleteEmployee(30);
           return Responses.SimpleWithData(x.status,x.detail);
        }
    }
}