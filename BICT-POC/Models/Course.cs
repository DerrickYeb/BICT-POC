using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICT_POC.Models
{
    public class Course
    {
        
        [DisplayName("Course Code")]
        [Column("CourseId")]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public ICollection<Days> Day { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
