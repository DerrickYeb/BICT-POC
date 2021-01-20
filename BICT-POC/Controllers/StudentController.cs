﻿using BICT_POC.Models;
using BICT_POC.Models.ViewModels;
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
        string BaseUrl = "https://localhost:44331/";
        private IEnumerable<Student> students = null;
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
        public ActionResult Assign(StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                if (studentVM.Student.Id != 0)
                {
                    context.SaveChanges();
                    //return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return HttpNotFound();
            }
            PopulateCourseDropdownList(studentVM.Student.CourseId);
            return View(studentVM);
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
            StudentVM studentVM = new StudentVM()
            {
                Student = new Student(),
                CoursesList = context.Courses.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                }),
               
            };
            
            if (id == null)
            {
                return View(studentVM);
            }
            studentVM.Student = context.Students.Find(id.GetValueOrDefault());
            if (TryUpdateModel(studentVM.Student,"",new string[] { "Course"}))
            {
                context.SaveChanges();
                //return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Assigning a course to a student failed");
            }
            PopulateCourseDropdownList(studentVM.Student.CourseId);
            return View(studentVM);
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
        public ActionResult DropDnAssigned()
        {

            return View();
        }
        #endregion
        public ActionResult GetStudents()
        {
            return Json(context.Students.Select(x => new
            {

                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender,
                Guidian = x.Guidian,
                GuideanContact = x.GuideanContact,
                AcademicYear = x.AcademicYear,
                Class = x.Class,
                Course = x.Courses
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}