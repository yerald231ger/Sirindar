using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.Configuration;
using System;

namespace Sirindar.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
       
        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public int Telefono { get; set; }

        [Required]
        [EmailAddress]
        public override string Email
        {
            get
            {
                return base.Email;
            }
            set
            {
                base.Email = value;
            }
        }

        [DataType(DataType.DateTime)]
        public Nullable<DateTime> FechaAlta { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<DateTime> FechaModificacion { get; set; } 

        public bool EsActivo { get; set; }
    }

    public class SirindarDbContext : IdentityDbContext<ApplicationUser>
    {
        public SirindarDbContext()
            : base("sirindarConnectionString")
        {
        }
    }

    public class SirindarDbContextInizializer : CreateDatabaseIfNotExists<SirindarDbContext>
    {
        protected override void Seed(SirindarDbContext context)
        {            
            context.Roles.Add(new IdentityRole
            {
                Id = "A",
                Name = "Admin"
            });
            context.Roles.Add(new IdentityRole
            {
                Id = "U",
                Name = "User"
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }


}