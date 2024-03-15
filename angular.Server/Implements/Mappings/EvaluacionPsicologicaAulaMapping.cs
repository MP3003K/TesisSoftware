using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class EvaluacionPsicologicaAulaMapping : IEntityTypeConfiguration<EvaluacionPsicologicaAula>
    {
        public void Configure(EntityTypeBuilder<EvaluacionPsicologicaAula> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Aula)
                .WithMany(x => x.EvaluacionesPsicologicasAula)
                .HasForeignKey(x => x.AulaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Unidad)
                .WithMany(x => x.EvaluacionesPsicologicasAula)
                .HasForeignKey(x => x.UnidadId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.EvaluacionPsicologica)
                .WithMany(x => x.EvaluacionesPsicologicasAula)
                .HasForeignKey(x => x.EvaluacionPsicologicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.FechaInicio).IsRequired(false);
            builder.Property(x => x.FechaFin).IsRequired(false);

            builder.Property(x => x.Estado).HasComment("(No inicio= N, En proceso = P, Finalizo= F)");

            builder.ToTable("EvaluacionesPsicologicasAula");
        }
    }
}
