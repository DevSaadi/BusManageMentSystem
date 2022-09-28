using FialBP.Models;
using FinalProject.Libs.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FialBP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //Login
        private Model1 db = new Model1();
        public ActionResult Login()
        {
            return View();
        }

        // POST: HomeController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,UserPass")] User model)
        {
            try
            {
                var oTblUser = db.Users.Where(o => o.Username == model.Username && o.UserPass == model.UserPass).FirstOrDefault();
                if (oTblUser != null)
                {
                    var listTblUserRole = db.UserRoles.Where(o => o.UserID == oTblUser.UserID).ToList();
                    Session["Users"] = oTblUser;
                    Session["UserRoles"] = listTblUserRole;
                    if (oTblUser.UserType == UserType.SuperAdmin.ToString())
                    {
                        return RedirectToAction("Index", "Users");
                    }
                    else if (oTblUser.UserType == UserType.GeneralUser.ToString())
                    {
                        return RedirectToAction("Index", "Buses");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("Users");
            return RedirectToAction("Index", "Home");
        }

    }
}