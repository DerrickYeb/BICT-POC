using BICT_POC.Models;
using SchoolAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BICT_POC.Controllers.api
{
    public class CourseController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public  IEnumerable<Course> GetCourses()
        {
            return context.Courses.ToList();

        }
        public Course GetCourse(int id)
        {
            var course = context.Courses.Where(x => x.Id == id).FirstOrDefault();
            return course;
        }

        [HttpPost]
        public HttpResponseMessage Create(Course course)
        {
            try
            {
                context.Courses.Add(course);
                context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateCourse(int Id,Course course)
        {
            try
            {
                if (Id == course.Id)
                {
                    context.Entry(course).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotModified);
                }
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage DeleteCourse(int id)
        {
            Course course = context.Courses.Find(id);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
