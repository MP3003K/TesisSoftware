using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class AccesoMapping : IEntityTypeConfiguration<Acceso>
    {
        public void Configure(EntityTypeBuilder<Acceso> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Accesos");
        }
    }
}
