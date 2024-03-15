using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class DimensionPsicologicaMapping : IEntityTypeConfiguration<DimensionPsicologica>
    {
        public void Configure(EntityTypeBuilder<DimensionPsicologica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.EvaluacionPsicologica)
                .WithMany(x => x.DimensionesPsicologicas)
                .HasForeignKey(x => x.EvaluacionPsicologicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("DimensionesPsicologicas");
        }
    }
}
