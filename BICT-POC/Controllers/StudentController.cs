using BICT_POC.Models;
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
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("api/student/");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var studentResp = responseMessage.Content.ReadAsStringAsync().Result;

                    students = JsonConvert.DeserializeObject<List<Student>>(studentResp);
                }
               
            }
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44331/api/student");

                    var postTask = client.PostAsJsonAsync<Student>("student", student);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }


            }
            else
            {
                ModelState.Clear();
            }
            return View(student);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var student = context.Students.Find(id);
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
            ViewBag.CourseId = new SelectList(course, "CourseId", "Name", selectCourse);
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
            if (studentVM.Student == null)
            {
                return HttpNotFound();
            }
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
                Course = x.Course.Title
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}