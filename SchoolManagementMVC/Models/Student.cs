using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "Nombre(s)")]
        [StringLength(20, ErrorMessage = "El campo {0} Permite solo {1}  y {2}.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellido(s)")]
        [StringLength(20, ErrorMessage = "El campo {0} Permite solo {1}  y {2}.", MinimumLength = 2)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} Permite solo {1} caácteres como máximo.")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [MaxLength(11, ErrorMessage = "El campo {0} Permite solo {1} caácteres como máximo.")]
        public string Phone { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} Permite solo {1} caácteres como máximo.")]
        public string Email { get; set; }

        [Display(Name = "Fecha de Entrada")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }

        [Required]
        [Display(Name = "Hora de Inicio")]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Hora de Salida")]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [NotMapped]
        [Display(Name = "Foto de Perfil")]
        public HttpPostedFileBase ImageUrl { get; set; }

        //public int DocumentTypeID { get; set; }

        //[Display(Name = "Número de Documento")]
        //public string DocumentNumber { get; set; }

        public int? StateID { get; set; }

        public int? PositionID { get; set; }

        public int? GenderID { get; set; }

        [Display(Name = "Comentarios")]
        [StringLength(160, ErrorMessage = "El campo {0} Permite solo {1} caácteres como máximo.")]
        public string Remarks { get; set; }

        //[Required]
        //[Display(Name = "Documento(s) Adjuntos")]
        //public string[] AttachedFiles { get; set; }

        //public virtual DocumentType DocumentType { get; set; }
        public virtual Position Positions { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual State State { get; set; }

        public virtual ICollection<UploadFile> UploadFiles { get; set; }
    }
}