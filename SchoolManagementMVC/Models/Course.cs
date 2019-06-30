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

        //public virtual ICollection<Student> Students { get; set; }
    }
}