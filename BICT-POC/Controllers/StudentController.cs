using BICT_POC.Models;
using BICT_POC.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BICT_POC.Controllers
{

    public class StudentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Student
        public ActionResult Index()
        {
            var students = context.Students.ToList();
            return View(students);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var reponse = context.Students.ToList();
            return View(reponse);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                context.Students.Add(student);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.Clear();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(Student student)
        {
            if (ModelState.IsValid)
            {

                int id = (int)TempData["Id"];
                var studentData = context.Students.Where(x => x.Id == id).FirstOrDefault();
                if (studentData != null)
                {
                    
                    studentData.CourseId = student.CourseId;
                    context.Students.Attach(studentData);
                    context.Entry(studentData).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }

            }
            ViewBag.Id = new SelectList(context.Courses, "Id", "Title");
            return View(student);
        }
        public ActionResult Assign(int? id)
        {
            if (id != 0)
            {
                Student student = context.Students.Find(id.GetValueOrDefault());
                context.Students.Attach(student);
                context.SaveChanges();
                PopulateCourseDropdownList(student.CourseId);
                return View(student);
            }
            else
            {
                return View();
                
            }
            
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Student student = context.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }
        private void PopulateCourseDropdownList(object selectCourse = null)
        {
            var course = from d in context.Courses
                         orderby d.Title
                         select d;
            ViewBag.Id = new SelectList(course, "Id", "Title", selectCourse);
        }


        /// <summary>
        /// Using direct access from the database without using the web api
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCourses()
        {
            return Json(context.Courses.Select(x => new
            {
                Id = x.Id,
                Title = x.Title
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        #region Populating students into the dropdownlist
        [HttpGet, ActionName("StudentDrop")]
        public ActionResult GetStudentDrop()
        {
            var students = context.Students.ToList();
            return Json(new { data = students }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult GetStudents()
        {
            return Json(context.Students.Select(x => new
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        private void PopulateAssignedCourseData(Student student)
        {
            var allCourses = context.Courses;
            var students = new HashSet<int>(student.Courses.Select(c => c.Id));
            var viewModel = new List<AssignedCourse>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourse
                {
                    CourseID = course.Id,
                    Title = course.Title,
                    Assigned = students.Contains(course.Id)
                });
            }
            ViewBag.Courses = viewModel;
        }

        public void UpdateStudentCourses(string[] selectedCourses, Student student)
        {
            if (selectedCourses == null)
            {
                student.Courses = new List<Course>();
                return;
            }
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var selectCourses = new HashSet<int>
                (student.Courses.Select(c => c.Id));
            foreach (var course in context.Courses)
            {
                if (selectedCoursesHS.Contains(course.Id.ToString()))
                {
                    if (!selectCourses.Contains(course.Id))
                    {
                        student.Courses.Add(course);
                    }
                }
                else
                {
                    if (selectCourses.Contains(course.Id))
                    {
                        student.Courses.Remove(course);
                    }
                }
            }
        }
    }
}