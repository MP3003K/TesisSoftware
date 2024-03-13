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
    public class RolUsuarioMapping: IEntityTypeConfiguration<RolUsuario>
    {
        public void Configure(EntityTypeBuilder<RolUsuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.RolesUsuarios)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Rol)
                .WithMany(x => x.RolesUsuarios)
                .HasForeignKey(x => x.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("RolesUsuarios");
        }

    }
}
