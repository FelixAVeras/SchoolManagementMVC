using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementMVC.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Display(Name = "Curso")]
        public string Description { get; set; }

        [Display(Name = "Sección")]
        public string Section { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}