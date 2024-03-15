using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class GradoMapping : IEntityTypeConfiguration<Grado>
    {
        public void Configure(EntityTypeBuilder<Grado> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Nivel)
                .WithMany(x => x.Grados)
                .HasForeignKey(x => x.NivelId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Grados");
        }
    }
}
