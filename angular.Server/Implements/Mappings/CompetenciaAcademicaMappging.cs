using Entities;
using Microsoft.EntityFrameworkCore;

namespace Implements.Mappings
{
    public class CompetenciaAcademicaMappging : IEntityTypeConfiguration<CompetenciaAcademica>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CompetenciaAcademica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.MateriaAcademica)
                .WithMany(x => x.CompetenciasAcademicas)
                .HasForeignKey(x => x.MateriaAcademicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.EvaluacionesCompetenciasEstudiante)
                .WithOne(x => x.CompetenciaAcademica)
                .HasForeignKey(x => x.CompetenciaAcademicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("CompetenciasAcademicas");
        }
    }
}
