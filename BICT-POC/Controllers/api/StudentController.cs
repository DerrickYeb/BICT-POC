using BICT_POC.Models;
using BICT_POC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BICT_POC.Controllers.api
{
    
    public class StudentController : ApiController
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return _context.Students.ToList();
        }
        [HttpGet]
        public Student GetStudent(int id)
        {
            return _context.Students.Find(id);
        }

        [HttpPost]
        public HttpResponseMessage Create(Student student)
        {
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
               
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateStudent(int Id, Student student)
        {
            try
            {
                if (Id == student.Id)
                {
                    _context.Entry(student).State = System.Data.Entity.EntityState.Modified;
                   _context.SaveChanges();
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
        public HttpResponseMessage Delete(int id)
        {
            Student student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

    }
}
