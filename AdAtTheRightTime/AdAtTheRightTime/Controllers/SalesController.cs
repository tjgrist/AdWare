using AdAtTheRightTime.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Salesforce.Force;
using WebApplication9.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity.Owin;

namespace AdAtTheRightTime.Controllers
{
    public class SalesController : Controller
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
        // GET: GoogleTrends
        public ActionResult Index()
        {
            //var queries = db.Queries.Include(q => q.Business);
            //return View(queries.ToList());
            //
            List<QueryViewModel> queryViewModelList = new List<QueryViewModel>();
            var user = UserManager.FindById(User.Identity.GetUserId());
            foreach (var item in db.Queries)
            {
                if (user.BusinessId == item.BusinessId)
                {
                    QueryViewModel queryViewModel = new QueryViewModel();               
                    queryViewModel.Queries = item.Queries;
                    queryViewModel.BusinessId = item.BusinessId;
                    queryViewModelList.Add(queryViewModel);
                }
            }
            try
            {
                queryViewModelList[0].QueryBuiltString = queryQueries();
            }
            catch
            {
                QueryViewModel defaultQueryViewModel = new QueryViewModel();
                queryViewModelList.Add(defaultQueryViewModel);
                queryViewModelList[0].Queries = "";
                queryViewModelList[0].BusinessId = null;
                queryViewModelList[0].QueryBuiltString = "";
            }
            return View(queryViewModelList);
        }
        // GET: GoogleTrends/Details/5
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

        // GET: GoogleTrends/Create
        public ActionResult Create()
        {
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City");
            return View();
        }

        // POST: GoogleTrends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QueryId,Queries,BusinessId")] Query query)
        {
            if (ModelState.IsValid)
            {
                db.Queries.Add(query);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "City", query.BusinessId);
            return View(query);
        }
        // GET: GoogleTrends/Delete/5
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

        // POST: GoogleTrends/Delete/5
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
        public async Task<ActionResult> Chart()
        {
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            var sales = await client.QueryAsync<SaleViewModel>("SELECT Name, Total__c FROM Sale__c");
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.isAdmin = "No";

                if (isAdminUser())
                {
                    ViewBag.isAdmin = "Yes";
                }
                return View(sales.records);
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();

        }
        public string queryQueries()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            List<string> queriesList = new List<string>();
            int i = 1;
            int j = 0;
            string[] queryTransition = new string[] { ",+" };
            foreach (Query query in db.Queries)
            {
                if (query.BusinessId == user.BusinessId)
                {
                    queriesList.Add(Convert.ToString(query.Queries));
                }
            }
            List<string> queriesSemiBuiltList = new List<string>();
            foreach (string query in queriesList)
            {
                queriesSemiBuiltList.Add(query.Replace(' ', '+'));
                if (i < queriesList.Count)
                {
                    queriesSemiBuiltList[j] = queriesSemiBuiltList[j] + ",+";
                }
                i++;
                j++;
            }
            string queriesBuiltString = "";
            foreach (string query in queriesSemiBuiltList)
            {
                queriesBuiltString += query;
            }
            return queriesBuiltString;
        }

    }
}