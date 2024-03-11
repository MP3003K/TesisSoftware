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
    public class UsuarioMapping : IEntityTypeConfiguration <Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Persona)
                .WithOne(x => x.Usuario)
                .HasForeignKey<Usuario>(x => x.PersonaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Usuarios");
        }   
    }
}
