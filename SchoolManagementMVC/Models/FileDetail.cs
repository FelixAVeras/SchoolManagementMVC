using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class FileDetail
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int SupportId { get; set; }
        public virtual UploadFile UploadFile { get; set; }
    }
}