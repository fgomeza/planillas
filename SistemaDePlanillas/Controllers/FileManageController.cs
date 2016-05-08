using SistemaDePlanillas.Models;
using SistemaDePlanillas.Models.Manager;
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
                foreach (var r in registers)
                    DBManager.Instance.employees.addCall(r.cmsid, r.calls, r.hours, r.date);
                return Json(registers);
            }
            catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "El archivo no tiene el formato esperado, no puede ser procesado");
            }
        }

    }
}