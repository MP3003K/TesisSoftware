using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class IndicadorPsicologicoMapping : IEntityTypeConfiguration<IndicadorPsicologico>
    {
        public void Configure(EntityTypeBuilder<IndicadorPsicologico> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.EscalaPsicologica)
                .WithMany(x => x.IndicadoresPsicologicos)
                .HasForeignKey(x => x.EscalaPsicologicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("IndicadoresPsicologicos");
        }
    }
}
