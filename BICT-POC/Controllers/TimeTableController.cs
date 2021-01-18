using BICT_POC.Models;
using SchoolAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BICT_POC.Controllers
{
    public class TimeTableController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        IEnumerable<TimeTable> timeTables = null;
        // GET: TimeTable
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44331/api/timeTable");

                var responseTask = client.GetAsync("timetable");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TimeTable>>();
                    readTask.Wait();

                    timeTables = readTask.Result;
                }
                else
                {
                    timeTables = Enumerable.Empty<TimeTable>();
                    ModelState.AddModelError(string.Empty, "Server error. Please check connection");
                }
            }
            return View(timeTables);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeTable timeTable)
        {
            if (ModelState.IsValid)
            {
              var result =  context.TimeTables.Add(timeTable);
                if (result != null)
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(timeTable);
        }
        public ActionResult GetCourses()
        {
            return Json(context.Courses.Select(x => new
            {
                Id = x.Id,
                Title = x.Title
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}