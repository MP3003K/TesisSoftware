using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Repository.Mappings
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
