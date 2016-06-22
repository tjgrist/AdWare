using AdAtTheRightTime.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdAtTheRightTime.Controllers
{
    public class SettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
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
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        // GET: Settings for changing Google Trends queries as well as linking Salesforce data;
        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.allowManagerAdd = "No";
                if (isAdminUser())
                {
                    ViewBag.allowManagerAdd = "Yes";
                }
                return View();
            }
            else
            {
                ViewBag.Message = "Not Logged IN";
            }
            return View();
        }
        public Boolean isAdminUser()
        {
             if (User.Identity.IsAuthenticated)
                {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString().Contains("Admin"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        [Authorize]
        [AllowAnonymous]
        public ActionResult RegisterManager()
        {
            ViewBag.Name = new SelectList(db.Roles.Where(x => x.Name.Contains("Manager")).ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterManager(RegisterViewModel model)
        {
            var admin = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, BusinessId = admin.BusinessId };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    return RedirectToAction("Index", "Settings");                 

                }
                ViewBag.Name = new SelectList(db.Roles.Where(x => x.Name.Contains("Manager")).ToList(), "Name", "Name");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}