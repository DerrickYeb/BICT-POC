﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BICT_POC.Models.ViewModel
{
    public class TimeTableVM
    {
        public TimeTable TimeTable { get; set; }
        public IEnumerable<SelectListItem> CoursesList { get; set; }
        public Student Student { get; set; }
    }
}