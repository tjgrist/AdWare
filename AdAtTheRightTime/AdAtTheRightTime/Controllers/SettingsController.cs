using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdAtTheRightTime.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings for changing Google Trends queries as well as linking Salesforce data;
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}