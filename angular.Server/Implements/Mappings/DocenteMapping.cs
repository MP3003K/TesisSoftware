using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class DocenteMapping : IEntityTypeConfiguration<Docente>
    {
        public void Configure(EntityTypeBuilder<Docente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Persona)
                .WithOne(x => x.Docente)
                .HasForeignKey<Docente>(x => x.PersonaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Docentes");
        }
    }
}
