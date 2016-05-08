using SistemaDePlanillas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    public class FileManageController : Controller
    {
        [HttpPost]
        public ActionResult uploadCmsFile(HttpPostedFileBase file)
        {
            try
            {
                var registers = FilesReader.readFromCMSFile(file.InputStream);
                //DBManager.Instance.addCalls(registers);
                return Json(registers);
            }
            catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "El archivo no tiene el formato esperado, no puede ser procesado");
            }
        }

    }
}