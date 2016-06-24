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
            var currentUser = db.Users.Find(user.GetUserId());
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
        [Authorize]
        public ActionResult AddBusinesses()
        {
            ViewBag.Name = new SelectList(db.Businesses.Distinct().ToList(), "Name", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddBusinesses(Business business)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            return View();
        }
        public ActionResult Cal()
        {
            return View();
        }
        
    }
}