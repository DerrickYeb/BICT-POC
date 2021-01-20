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
    }
}