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
using Microsoft.AspNet.Identity.Owin;

namespace AdAtTheRightTime.Controllers
{
    public class QueriesController : Controller
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

        public ActionResult ViewQueries()
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Find(Id);
            int? id = currentUser.BusinessId;
            var s = UserManager.GetRoles(Id);
            string role = s[0].ToString();
            ViewBag.Role = role;
            var queries = (from query in db.Queries where query.BusinessId == id select query);
            if(queries.ToList().Count == 0)
            {
                ViewBag.Message = "empty";
            }
            else
            {
                ViewBag.Message = "notEmpty";
            }
            return View(queries);
        }

        // GET: Queries
        public ActionResult Index()
        {
            var queries = db.Queries.Include(q => q.Business);
            return View(queries.ToList());
        }

        // GET: Queries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Query query = db.Queries.Find(id);
            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
        }

        // GET: Queries/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Queries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Create([Bind(Include = "QueryId,Queries")] Query query)
        {
            if (ModelState.IsValid)
            {
                var Id = User.Identity.GetUserId();
                var currentUser = db.Users.Find(Id);
                query.BusinessId = currentUser.BusinessId;
                db.Queries.Add(query);
                db.SaveChanges();
                
            }

            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City", query.BusinessId);
            
        }

        // GET: Queries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Query query = db.Queries.Find(id);
            if (query == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City", query.BusinessId);
            return View(query);
        }

        // POST: Queries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QueryId,Queries,BusinessId")] Query query)
        {
            if (ModelState.IsValid)
            {
                db.Entry(query).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewQueries", new { id = query.BusinessId });
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City", query.BusinessId);
            return View(query);
        }

        // GET: Queries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Query query = db.Queries.Find(id);
            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
        }

        // POST: Queries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Query query = db.Queries.Find(id);
            db.Queries.Remove(query);
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
        public ActionResult ChooseQuery()
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Find(Id);
            int? id = currentUser.BusinessId;
            var s = UserManager.GetRoles(Id);
            string role = s[0].ToString();
            ViewBag.Role = role;
            var queries = (from query in db.Queries where query.BusinessId == id select query);
            if (queries.ToList().Count == 0)
            {
                ViewBag.Message = "empty";
            }
            else
            {
                ViewBag.Message = "notEmpty";
            }
            return View(queries);
        }
    }
}
