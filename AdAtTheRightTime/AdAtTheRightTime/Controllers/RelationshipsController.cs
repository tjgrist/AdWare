using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdAtTheRightTime.Models;

namespace AdAtTheRightTime.Controllers
{
    public class RelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Relationships
        public ActionResult Index()
        {
            var relationships = db.Relationships.Include(r => r.Business);
            return View(relationships.ToList());
        }

        // GET: Relationships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationships relationships = db.Relationships.Find(id);
            if (relationships == null)
            {
                return HttpNotFound();
            }
            return View(relationships);
        }

        // GET: Relationships/Create
        public ActionResult Create()
        {
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City");
            return View();
        }

        // POST: Relationships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RelationshipId,BusinessId,UserId")] Relationships relationships)
        {
            if (ModelState.IsValid)
            {
                db.Relationships.Add(relationships);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City", relationships.BusinessId);
            return View(relationships);
        }

        // GET: Relationships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationships relationships = db.Relationships.Find(id);
            if (relationships == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City", relationships.BusinessId);
            return View(relationships);
        }

        // POST: Relationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RelationshipId,BusinessId,UserId")] Relationships relationships)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relationships).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City", relationships.BusinessId);
            return View(relationships);
        }

        // GET: Relationships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationships relationships = db.Relationships.Find(id);
            if (relationships == null)
            {
                return HttpNotFound();
            }
            return View(relationships);
        }

        // POST: Relationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relationships relationships = db.Relationships.Find(id);
            db.Relationships.Remove(relationships);
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
    }
}
