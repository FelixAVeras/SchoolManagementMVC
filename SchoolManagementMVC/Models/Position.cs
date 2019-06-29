using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class Position
    {
        [Key]
        public int PositionID { get; set; }

        [Display(Name = "Estado")]
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}