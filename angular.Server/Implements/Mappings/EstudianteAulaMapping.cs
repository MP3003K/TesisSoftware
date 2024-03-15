using Entities;
using Microsoft.EntityFrameworkCore;

namespace Implements.Mappings
{
    public class EstudianteAulaMapping : IEntityTypeConfiguration<EstudianteAula>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EstudianteAula> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Estudiante)
                .WithMany(x => x.EstudiantesAulas)
                .HasForeignKey(x => x.EstudianteId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Aula)
                .WithMany(x => x.EstudiantesAulas)
                .HasForeignKey(x => x.AulaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("EstudiantesAulas");
        }
    }
}
