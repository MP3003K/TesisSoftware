using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class EstudianteMapping : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Estudiante> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Persona)
                .WithOne(x => x.Estudiante)
                .HasForeignKey<Estudiante>(x => x.PersonaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Aula)
                .WithMany(x => x.Estudiantes)
                .HasForeignKey(x => x.AulaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Estudiantes");
        }
    }
}
