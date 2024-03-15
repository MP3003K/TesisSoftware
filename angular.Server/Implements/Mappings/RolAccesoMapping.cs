using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class RolAccesoMapping : IEntityTypeConfiguration<RolAcceso>
    {
        public void Configure(EntityTypeBuilder<RolAcceso> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Rol)
                .WithMany(x => x.RolesAccesos)
                .HasForeignKey(x => x.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Acceso)
                .WithMany(x => x.RolesAccesos)
                .HasForeignKey(x => x.AccesoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("RolesAccesos");

        }
    }
}
