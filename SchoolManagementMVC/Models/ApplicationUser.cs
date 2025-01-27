using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagementMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }  // Nombre completo del usuario
        public string Role { get; set; }      // Rol personalizado, por ejemplo: Estudiante, Profesor
        public DateTime DateOfBirth { get; set; }  // Fecha de nacimiento
        public string Address { get; set; }   // Dirección del usuario
        public DateTime DateOfAdmission { get; set; } // Fecha de admisión (para estudiantes)
    }
}