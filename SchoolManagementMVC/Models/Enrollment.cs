using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [Required]
        [Display(Name = "Fecha de Inscripción")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public int StateID { get; set; }

        public virtual Student Students { get; set; }
        public virtual Course Course { get; set; }
        public virtual State State { get; set; }
    }
}