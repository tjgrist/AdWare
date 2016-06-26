//using AdAtTheRightTime.Models;
//using DayPilot.Web.Ui;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace AdAtTheRightTime.Controllers
//{
//    public class EventsController : Controller
//    {
//        ApplicationDbContext db = new ApplicationDbContext();
//        // GET: Events
//        public ActionResult Events(DateTime? start, DateTime? end)
//        {

//            // SQL: SELECT * FROM [event] WHERE NOT (([end] <= @start) OR ([start] >= @end))
//            var events = from ev in db.Events.AsEnumerable() where !(Convert.ToDateTime(ev.end) <= start || Convert.ToDateTime(ev.start) >= end) select ev;

//            var result = events
//            .Select(e => new JsonEvent()
//            {
//                start = e.start.ToString(),
//                end = e.end.ToString(),
//                text = e.name,
//                id = e.id.ToString()
//            })
//            .ToList();

//            return new JsonResult { Data = result };
//        }

        //public ActionResult Create(string start, string end, string name)
        //{
        //    var toBeCreated = new Event
        //    {
        //        start = Convert.ToDateTime(start),
        //        end = Convert.ToDateTime(end),
        //        name = name
        //    };
        //    db.Events.Add(toBeCreated);
        //    db.SaveChanges();
        //    //db.Events.InsertOnSubmit(toBeCreated);
        //    //db.SubmitChanges();

        //    return new JsonResult { Data = new Dictionary<string, object> { { "id", toBeCreated.id } } };

        //}

        //public ActionResult Move(int id, string newStart, string newEnd)
        //{
        //    var toBeResized = (from ev in db.Events where ev.id == id select ev).First();
        //    toBeResized.start = Convert.ToDateTime(newStart);
        //    toBeResized.end = Convert.ToDateTime(newEnd);
        //    db.SubmitChanges();

        //    return new JsonResult { Data = new Dictionary<string, object> { { "id", toBeResized.id } } };
        //}

        //public ActionResult Resize(int id, string newStart, string newEnd)
        //{
        //    var toBeResized = (from ev in db.Events where ev.id == id select ev).First();
        //    toBeResized.start = Convert.ToDateTime(newStart);
        //    toBeResized.end = Convert.ToDateTime(newEnd);
        //    db.SubmitChanges();

        //    return new JsonResult { Data = new Dictionary<string, object> { { "id", toBeResized.id } } };
        //}
//    }
//}
