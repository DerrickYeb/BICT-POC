using BICT_POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BICT_POC.Controllers
{

    public class CourseController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        //  private IEnumerable<Course> courses;
        // GET: Course

        public ActionResult Index()
        {
            var courses = context.Courses.ToList();
            return View(courses);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
     [HttpPost]
     [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Courses.Add(course);
                context.SaveChanges();
                RedirectToAction(nameof(Index));
            }
            else
            {
                return HttpNotFound();
            }
            return View(course);
        }
        public ActionResult Edit(int? Id)
        {
            var course = context.Courses.Where(x => x.Id == Id).FirstOrDefault();
            if (course != null)
            {
                TempData["Id"] = Id;
                TempData.Keep();
                return View(course);
            }
            return View();

        }
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(Course course)
        {
            int id = (int)TempData["Id"];
            var editData = context.Courses.Where(x => x.Id == id).FirstOrDefault();
            if (editData != null)
            {
                editData.Title = course.Title;
                editData.Day = course.Day;
                context.Entry(editData).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                ViewBag.Days = course.Day;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}