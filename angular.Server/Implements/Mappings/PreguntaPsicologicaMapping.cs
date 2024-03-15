using Entities;
using Microsoft.EntityFrameworkCore;

namespace Implements.Mappings
{
    public class PreguntaPsicologicaMapping : IEntityTypeConfiguration<PreguntaPsicologica>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PreguntaPsicologica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.IndicadorPsicologico)
                .WithMany(x => x.PreguntasPsicologicas)
                .HasForeignKey(x => x.IndicadorPsicologicoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("PreguntasPsicologicas");
        }
    }
}
