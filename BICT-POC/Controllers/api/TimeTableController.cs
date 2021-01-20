using BICT_POC.Models;
using SchoolAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BICT_POC.Controllers
{
    public class TimeTablesController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        [HttpGet]
        public IEnumerable<TimeTable> GetTimeTables()
        {
            var timeTables = _context.TimeTables.ToList();
            return timeTables;

        }
        [HttpGet]
        public TimeTable GetTimeTable(int id)
        {
            var timetable = _context.TimeTables.Where(x => x.Id == id).FirstOrDefault();
            return timetable;
        }

        [HttpPost]
        public HttpResponseMessage Create(TimeTable TimeTable)
        {
            try
            {
                _context.TimeTables.Add(TimeTable);
                _context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateTimeTable(int Id, TimeTable TimeTable)
        {
            try
            {
                if (Id == TimeTable.Id)
                {
                    _context.Entry(TimeTable).State = System.Data.Entity.EntityState.Modified;
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
        public HttpResponseMessage DeleteCourse(int id)
        {
            TimeTable TimeTable = _context.TimeTables.Find(id);
            if (TimeTable != null)
            {
                _context.TimeTables.Remove(TimeTable);
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
