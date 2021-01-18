﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICT_POC.Models
{
    public class TimeTable
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [Required]
        public Course Course { get; set; }
        [Required]
        public string Time { get; set; }
        
        public string Day { get; set; }
    }
}
