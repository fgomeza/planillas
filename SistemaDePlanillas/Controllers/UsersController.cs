
using SistemaDePlanillas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string name = collection["name"];
                string username = collection["username"];
                string password = collection["password"];
                int role = Int32.Parse(collection["role"]);
                int location = Int32.Parse(collection["location"]);
                string email = collection["email"];

                DBManager db = DBManager.Instance;
                Result<string> result = db.addUser(name, username, password, role, location, email);
                Console.WriteLine(result.detail);
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch(FormatException ex)
            {
                Console.WriteLine("Format exception. UsersController: Create()");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            DBManager db = DBManager.Instance;
            Result<User> result = db.selectUser(id);
            

            // Aquí poner el user obtenido (result.detail) en la página, posiblemente mediante 'Request'
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                DBManager db = DBManager.Instance;
                // validar si collection trae los parámetros corretos
                Result<string> result = db.deleteUser(id);
                Console.WriteLine(result.detail);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
