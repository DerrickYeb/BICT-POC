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
            var timeTables = context.TimeTables.ToList();
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
            PopulateCourseDropdownList(timeTable.TimeTable.CourseId);
            PopulateStudentsDropdownList(timeTable.Student.Id);
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
        private void PopulateStudentsDropdownList(object selectStudent = null)
        {
            var student = from d in context.Students
                         orderby d.FullName
                         select d;
            ViewBag.StudentId = new SelectList(student, "Id", "FullName", selectStudent);
        }
    }
}