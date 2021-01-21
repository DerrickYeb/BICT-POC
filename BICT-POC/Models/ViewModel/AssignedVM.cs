using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BICT_POC.Models.ViewModel
{
    public class AssignedVM
    {
        public Course Course { get; set; }
        public IEnumerable<SelectListItem> CoursesList { get; set; }
        public Student Student { get; set; }
        public IEnumerable<SelectListItem> StudentsList { get; set; }
    }
}