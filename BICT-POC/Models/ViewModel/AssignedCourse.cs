using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BICT_POC.Models.ViewModel
{
    public class AssignedCourse
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}