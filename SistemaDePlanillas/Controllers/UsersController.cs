
using SistemaDePlanillas.Models;
using SistemaDePlanillas.Models.Operations;
using SistemaDePlanillas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            User user = SessionManager.Instance.getUser(Session);
            if(user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var result = DBManager.Instance.selectAllUsers(user.Location);
            if(result.status == 0)
            {
                List<UserViewModel> UsersList = new List<UserViewModel>();
                foreach (User Item in result.detail)
                {
                    UserViewModel UserViewModel = new UserViewModel()
                    {
                        Username = Item.Username,
                        Name = Item.Name,
                        Email = Item.Email,
                        /* Actualmente Role y Location son números.
                         * Se debe crear una lógica que convierta este índice en el string
                         * que será renderizado en la vista.
                         */
                    };
                    UsersList.Add(UserViewModel);
                }
                GetUsersViewModel ViewModel = new GetUsersViewModel()
                {
                    Users = UsersList
                };
                return View(ViewModel.Users);
            }
            else
            {
                ViewBag.Message = "Error code: " + result.status;
                return View();
            }
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
        public ActionResult Create(UserCreateViewModel model)
        {
            try
            {
                DBManager db = DBManager.Instance;
                Result<string> result = db.addUser(model.Name, model.Username, model.Password, model.Role, model.Location, model.Email);
                Console.WriteLine(result.detail);

                return RedirectToAction("Index");
            }
            catch(FormatException)
            {
                string error = "Format exception. UsersController: Create()";
                Console.WriteLine(error);
                ViewBag.Error = error;
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
