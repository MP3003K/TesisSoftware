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
    public class DocenteMapping : IEntityTypeConfiguration <Docente>
    {
        public void Configure(EntityTypeBuilder<Docente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Persona)
                .WithOne(x => x.Docente)
                .HasForeignKey<Docente>(x => x.PersonaId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.ToTable("Docentes");
        }
    }
}
