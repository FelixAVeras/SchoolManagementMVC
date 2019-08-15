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
        public int UploadFileID { get; set; }

        //[Required(ErrorMessage = "Please Enter Your Name")]
        //[Display(Name = "Name")]
        //[MaxLength(100)]
        //public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter a Comentary")]
        [Display(Name = "Comentario")]
        [MaxLength(500)]
        public string Summary { get; set; }

        public int? StudentID { get; set; }
        public int? EmployeeID { get; set; }

        public virtual Student Students { get; set; }
        public virtual Employee Employees { get; set; }

        public virtual ICollection<FileDetail> FileDetails { get; set; }
    }
}