using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class MateriaAcademicaMapping : IEntityTypeConfiguration<MateriaAcademica>
    {
        public void Configure(EntityTypeBuilder<MateriaAcademica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.CompetenciasAcademicas)
                .WithOne(x => x.MateriaAcademica)
                .HasForeignKey(x => x.MateriaAcademicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("MateriasAcademicas");
        }
    }
}
