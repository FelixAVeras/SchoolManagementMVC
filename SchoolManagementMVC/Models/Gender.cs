using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class Gender
    {
        [Key]
        public int GenderID { get; set; }

        [Display(Name = "Genero")]
        [StringLength(20, ErrorMessage = "El campo {0}, solo puede contener {1} como máximo.")]
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}