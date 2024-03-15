using Entities;
using Microsoft.EntityFrameworkCore;

namespace Implements.Mappings
{
    public class EstudianteMapping : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Estudiante> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Persona)
                .WithOne(x => x.Estudiante)
                .HasForeignKey<Estudiante>(x => x.PersonaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Estudiantes");
        }
    }
}
