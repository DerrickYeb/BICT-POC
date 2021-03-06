﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BICT_POC.Models.ViewModel
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public IEnumerable<SelectListItem> StudentLists { get; set; }
        public IEnumerable<SelectListItem> CoursesList { get; set; }
    }
}