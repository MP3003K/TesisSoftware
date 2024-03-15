using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class EscalaPsicologicaMapping : IEntityTypeConfiguration<EscalaPsicologica>
    {
        public void Configure(EntityTypeBuilder<EscalaPsicologica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.DimensionPsicologica)
                .WithMany(x => x.EscalasPsicologicas)
                .HasForeignKey(x => x.DimensionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("EscalasPsicologicas");
        }
    }
}
