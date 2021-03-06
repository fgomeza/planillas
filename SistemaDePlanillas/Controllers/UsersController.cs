﻿
using SistemaDePlanillas.Filters;
using SistemaDePlanillas.Models;
using SistemaDePlanillas.Models.Manager;
using SistemaDePlanillas.Models.Operations;
using SistemaDePlanillas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    [AjaxOnly]
    public class UsersController : Controller
    {
        // GET: Users
        /*
        public ActionResult Index()
        {
            { User user = SessionManager.Instance.getUser(Session);
            if(user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // TODO: Hay que dehacerse del código de aquí arriba

            var result = DBManager.Instance.selectAllUsers(user.Location);
            if(result.Status == 0)
            {
                List<UserViewModel> UsersList = new List<UserViewModel>();
                foreach (User Item in result.Detail)
                {
                    UserViewModel UserViewModel = new UserViewModel()
                    {
                        Username = Item.Username,
                        Name = Item.Name,
                        Email = Item.Email,
                        PrimaryKey = Item.Id,
                        Role = Convert.ToString(Item.Role),
                        Location = Convert.ToString(Item.Location)
                        /* Actualmente Role y Location son números.
                         * Se debe crear una lógica que convierta este índice en el string
                         * que será renderizado en la vista.
                         
                    };
                    UsersList.Add(UserViewModel);
                }
                }
                return PartialView(UsersList);

            }
            else
            {
                ViewBag.Message = "Error code: " + result.Status;
                return View();
            }
        }
*/

        // GET: Users/Details/5
        public ActionResult Details(long id)
        {
            User User = DBManager.Instance.users.selectUser(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            UserViewModel viewModel = new UserViewModel
            {
                PrimaryKey = User.Id,
                Name = User.Name,
                Username = User.Username,
                Role = Convert.ToString(User.Role),
                Location = Convert.ToString(User.Location),
                Email = User.Email
            };

            return PartialView(viewModel);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(UserCreateViewModel model)
        {
            try
            {
                DBManager db = DBManager.Instance;
                db.users.addUser(model.Name, model.Username, model.Password, model.Role, model.Location, model.Email);

                return RedirectToAction("Index");
            }
            catch(FormatException)
            {
                string error = "Format exception. UsersController: Create()";
                Console.WriteLine(error);
                ViewBag.Error = error;
                return PartialView();
            }
            catch
            {
                return PartialView();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(long id)
        {
            return PartialView();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return PartialView();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(long id)
        {
            return Details(id);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, FormCollection collection)
        {
            User user = DBManager.Instance.users.selectUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //DBManager.Instance.deleteUser(user);
            DBManager.Instance.users.deleteUser(id);
            //db.SaveChanges(); // --> Commit;
            return RedirectToAction("Index");
        }
    }
}
