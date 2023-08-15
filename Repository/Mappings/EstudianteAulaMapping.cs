using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class EstudianteAulaMapping : IEntityTypeConfiguration<EstudianteAula>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EstudianteAula> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Estudiante)
                .WithMany(x => x.EstudiantesAulas)
                .HasForeignKey(x => x.EstudianteId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Aula)
                .WithMany(x => x.EstudiantesAulas)
                .HasForeignKey(x => x.AulaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Anio).IsRequired(false);

            builder.ToTable("EstudiantesAulas");
        }
    }
}
