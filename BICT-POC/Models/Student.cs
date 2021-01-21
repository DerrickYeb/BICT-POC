using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BICT_POC.Models
{
    public class Student : RegisterModel
    {
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime DateEnrolled { get; set; }
        public int? CourseId { get; set; }
        [DisplayName("Time Table")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
       public virtual ICollection<Course> Courses { get; set; }
      
    }
}
