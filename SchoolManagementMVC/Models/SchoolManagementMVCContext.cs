using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SchoolManagementMVC.Models
{
    public class SchoolManagementMVCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SchoolManagementMVCContext() : base("name=SchoolManagementMVCContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<UploadFile> UploadFiles { get; set; }
    }
}
