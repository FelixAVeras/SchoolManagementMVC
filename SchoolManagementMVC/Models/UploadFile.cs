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

        [Display(Name = "Comentario")]
        [MaxLength(500)]
        public string Summary { get; set; }

        public int PositionID { get; set; }
        public int? StudentID { get; set; }
        public int? EmployeeID { get; set; }

        public virtual Student Students { get; set; }
        public virtual Position Position { get; set; }
        public virtual Employee Employees { get; set; }

        public virtual ICollection<FileDetail> FileDetails { get; set; }
    }
}