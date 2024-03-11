using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings
{
    public class UnidadMapping : IEntityTypeConfiguration<Unidad>
    {
        public void Configure(EntityTypeBuilder<Unidad> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Escuela)
                .WithMany(x => x.Unidades)
                .HasForeignKey(x => x.EscuelaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Unidades");
        }
    }
}
