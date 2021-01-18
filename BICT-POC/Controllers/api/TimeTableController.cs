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
        public HttpResponseMessage GetTimeTables()
        {
            var TimeTables = _context.TimeTables.ToList();
            return new HttpResponseMessage(HttpStatusCode.OK);

        }
        [HttpGet]
        public TimeTable GetTimeTable(int id)
        {
            return _context.TimeTables.Find(id);
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
