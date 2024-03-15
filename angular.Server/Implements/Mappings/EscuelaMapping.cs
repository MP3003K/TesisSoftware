using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class EscuelaMapping : IEntityTypeConfiguration<Escuela>
    {
        public void Configure(EntityTypeBuilder<Escuela> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Escuelas");
        }
    }
}
