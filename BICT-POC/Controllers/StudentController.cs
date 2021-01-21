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
                if (student.Id != 0)
                {
                    
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return HttpNotFound();
            }
            //PopulateCourseDropdownList(studentVM.Student.CourseId);
            ViewBag.Id = new SelectList(context.Courses, "Id", "Title", student.CourseId);
            return View(student);
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
       
        public ActionResult Assign(int? id)
        {
            Student student = new Student();
         
            if (id == null)
            {
                return View(student);
            }
            else if (id != 0)
            {
                student = context.Students.Find(id.GetValueOrDefault());
                if (TryUpdateModel(student, "", new string[] { "CourseId" }))
                {
                    context.SaveChanges();
                    //return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", "Assigning a course to a student failed");
                }
            }
            
            PopulateCourseDropdownList(student.CourseId);
            return View(student);
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
        //public ActionResult DropDnAssigned(Enrollment enrollment)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //}
        #endregion
        public ActionResult GetStudents()
        {
            return Json(context.Students.Select(x => new
            {
                Id = x.Id,
                Name = x.FirstName +" "+ x.LastName
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