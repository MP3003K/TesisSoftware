using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class RolAccesoMapping: IEntityTypeConfiguration<RolAcceso>
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
