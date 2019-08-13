using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class UploadFile
    {
        [Key]
        public int UploadingFileID { get; set; }

        [Display(Name = "Choose File(s)")]
        public HttpPostedFileBase[] files { get; set; }
        public int? StudentID { get; set; }
        public int? EmployeeID { get; set; }

        public virtual Student Students { get; set; }
        public virtual Employee Employees { get; set; }
    }
}