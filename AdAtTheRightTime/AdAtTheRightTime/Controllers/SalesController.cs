using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Salesforce.Force;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class SalesController : Controller
    {
        // GET: /Orders/
        public async Task<ActionResult> Index()
        {
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            var sales     = await client.QueryAsync<SaleViewModel>("SELECT Name, Total__c FROM Sale__c");

            return View(sales.records);
        }
    }
}