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
    public class Student
    {
        [Column("StudentId")]
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Guidian { get; set; }
        [Required]
        [DisplayName("Guidian Contact")]
        [Phone]
        public string GuideanContact { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DisplayName("Academic Year")]
        public string AcademicYear { get; set; }
        [Required,StringLength(20)]
        public string Address { get; set; }
        public Nullable<int> CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Courses { get; set; }
       
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }
}
