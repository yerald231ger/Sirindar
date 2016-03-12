using System.Data.Entity.ModelConfiguration;
using Sirindar.Core;

namespace Sirindar.Entity.EntityConfigurations
{
    public class AsistenciaConfiguration : EntityTypeConfiguration<Asistencia>
    {
        public AsistenciaConfiguration() 
        {
            ToTable("TblAsistencias");

            HasKey(a => a.AsistenciaId);

            Property(a => a.HoraAsistencia)
                .HasColumnType("DateTime");

            HasRequired(a => a.Deportista)
                .WithMany(d => d.Asistencias)
                .HasForeignKey(a => a.DeportistaId);

            HasRequired(a => a.Horario);
        }
    }
            //Property(c => c.Description)
            //    .IsRequired()
            //    .HasMaxLength(2000);

            //Property(c => c.Name)
            //    .IsRequired()
            //    .HasMaxLength(255);

            //HasRequired(c => c.Author)
            //    .WithMany(a => a.Courses)
            //    .HasForeignKey(c => c.AuthorId)
            //    .WillCascadeOnDelete(false);

            //HasRequired(c => c.Cover)
            //    .WithRequiredPrincipal(c => c.Course);

            //HasMany(c => c.Tags)
            //    .WithMany(t => t.Courses)
            //    .Map(m =>
            //    {
            //        m.ToTable("CourseTags");
            //        m.MapLeftKey("CourseId");
            //        m.MapRightKey("TagId");
            //    });
        
    
}
