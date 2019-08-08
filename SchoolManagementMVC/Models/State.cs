using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class State
    {
        [Key]
        public int StateID { get; set; }

        [Display(Name = "Estado")]
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Student> Students { get; set; }

    }
}