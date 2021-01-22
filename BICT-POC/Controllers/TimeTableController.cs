using BICT_POC.Models;
using BICT_POC.Models.ViewModel;
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

        // GET: TimeTable
        public ActionResult Index()
        {
            var timeTables = context.TimeTables.ToList();
            return View(timeTables);
        }
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(context.Courses, "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeTable timeTable)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Id = new SelectList(context.Courses, "Id", "Title", timeTable.CourseId);
                context.TimeTables.Add(timeTable);
                context.SaveChanges();
                RedirectToAction(nameof(Index));
            }
            else
            {
                return ViewBag("An error has occured");
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
        public ActionResult GetStudents()
        {
            return Json(context.Students.Select(x => new
            {
                id = x.Id,
                FullName = x.FullName
            }));
        }
        private void PopulateCourseDropdownList(object selectCourse = null)
        {
            var course = from d in context.Courses
                         orderby d.Title
                         select d;
            ViewBag.Id = new SelectList(course, "Id", "Title", selectCourse);
        }

    }
}