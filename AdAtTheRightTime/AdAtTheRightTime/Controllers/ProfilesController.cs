using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using AdAtTheRightTime.Models;


namespace AdAtTheRightTime.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Profiles
        public ActionResult Index()
        {
            return View();
        }

        // GET: Profiles/Details/5
        public ActionResult ViewProfileDetails()
        {

            var user = User.Identity;
            var s = UserManager.GetRoles(user.GetUserId());
            string role = s[0].ToString();
            switch (role)
            {
                case "Super Admin":
                    return RedirectToAction("Index", "Users");
                case "Business Admin":
                    return RedirectToAction("AdminView", "Businesses");
                case "Business Manager":
                    return RedirectToAction("ManagerView", "Businesses");
                default:
                    return RedirectToAction("UserView", "Users");


            }

        }
        //public ActionResult ViewStats()
        //{

        //}

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profiles/Edit/5
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

        // GET: Profiles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profiles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}