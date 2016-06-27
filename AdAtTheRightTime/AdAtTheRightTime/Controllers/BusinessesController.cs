using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdAtTheRightTime.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AdAtTheRightTime.Controllers
{
    public class BusinessesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Businesses
        public ActionResult Index()
        {
            return View(db.Businesses.ToList());
        }
        public ActionResult Search(string searchBy, string search)
        {
            if(searchBy == "City")
            {
                return View(db.Businesses.Where(x => x.City == search).ToList());
            }
            else if(searchBy == "Industry")
            {
                return View(db.Businesses.Where(x => x.Industry == search).ToList());
            }
            else
            {
                return View(db.Businesses.Where(x => x.Name == search).ToList());
            }
        }
        public ActionResult ViewLikedBusinesses()
        {
            var userId = User.Identity.GetUserId();
            var Businessids = from relationship in db.Relationships where relationship.UserId == userId select relationship.BusinessId;
            List<Business> likedBusinesses = new List<Business>();          
            likedBusinesses = db.Businesses.Where(x => Businessids.ToList().Contains(x.BusinessId)).ToList();
            return View(likedBusinesses);
        }
        public ActionResult ViewConnectedUsers()
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Find(Id);
            var Userids = from relationship in db.Relationships where relationship.BusinessId == currentUser.BusinessId select relationship.UserId;
            var connectedUsers = db.Users.Where(x => Userids.ToList().Contains(x.Id)).ToList();
            return View(connectedUsers);
        }

        // GET: Businesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: Businesses/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.isAdmin = "No";

                if (isAdminUser())
                {
                    ViewBag.isAdmin = "Yes";
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();
        }

        // POST: Businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessId,City,Name,Industry,Description,Promotion")] Business business)
        {
            if (ModelState.IsValid)
            {
                var Id = User.Identity.GetUserId();
                var currentUser = db.Users.Find(Id);

                db.Businesses.Add(business);
                db.SaveChanges();
                currentUser.BusinessId = business.BusinessId;
                db.SaveChanges();
                return RedirectToAction("AdminView", new {id = business.BusinessId});
            }

            return View(business);
        }

        // GET: Businesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessId,City,Name,Industry,Description,Promotion")] Business business)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.BusinessId == business.BusinessId)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(business).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Registered your business.";
                    return RedirectToAction("AdminView");
                }
                return View(business);
            }
            ViewBag.Message = "You cannot edit this business.";
            return View(business);
        }

        // GET: Businesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business business = db.Businesses.Find(id);
            db.Businesses.Remove(business);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
        public ActionResult AdminView()
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Find(Id);
            var id  = currentUser.BusinessId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }
        public ActionResult ManagerView()
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Find(Id);
            var id = currentUser.BusinessId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }
        public ActionResult UserView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }
        public ActionResult ViewManagers()
        {
            var Bid= User.Identity.GetUserId();
            var currentUser = db.Users.Find(Bid);
            int? id = currentUser.BusinessId;
            var managers = db.Users.Where(x => x.BusinessId == id).ToList();
            var managersInBusiness = managers.Where(x => x.Id != Bid).ToList();
            return View(managersInBusiness);

        }

    }
}
