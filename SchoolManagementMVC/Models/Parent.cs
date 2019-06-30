using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class Parent
    {
        //public Parent()
        //{
        //    this.Student = new HashSet<Student>();
        //}

        [Key]
        public int ParentID { get; set; }

        [Required]
        [Display(Name = "Nombre(s)")]
        [StringLength(20, ErrorMessage = "El campo {0} Permite solo {1}  y {2}.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellido(s)")]
        [StringLength(20, ErrorMessage = "El campo {0} Permite solo {1}  y {2}.", MinimumLength = 2)]
        public string LastName { get; set; }

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

        public int DocumentTypeID { get; set; }

        [Required]
        [Display(Name = "Número de Documento")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Genero")]
        public int GenderID { get; set; }
        


        public virtual DocumentType DocumentType { get; set; }
        public virtual Student Student { get; set; }
        public virtual Gender Gender { get; set; }
    }
}