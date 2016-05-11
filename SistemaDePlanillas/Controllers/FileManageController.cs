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
            List<FilesReader.CMSRegister> errors = new List<FilesReader.CMSRegister>();
            List<FilesReader.CMSRegister> registers;
            try
            {
                registers = FilesReader.readFromCMSFile(file.InputStream);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "El archivo no tiene el formato esperado, no puede ser procesado");
            }

            foreach (var r in registers)
            {
                try
                {
                    DBManager.Instance.calls.addCall(r.cmsid, r.calls, r.hours, r.date);
                }
                catch (Exception e)
                {
                    errors.Add(r);
                }
            }
            return Json(errors);

        }

    }
}