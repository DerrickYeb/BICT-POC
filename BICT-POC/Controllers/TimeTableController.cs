using BICT_POC.Models;
using BICT_POC.Models.ViewModels;
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
                client.BaseAddress = new Uri("https://localhost:44331/api/timeTables");

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
        public ActionResult Create(TimeTableVM timeTable)
        {
            if (ModelState.IsValid)
            {
                if (timeTable.TimeTable.Id == 0)
                {
                    context.TimeTables.Add(timeTable.TimeTable);
                }
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
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