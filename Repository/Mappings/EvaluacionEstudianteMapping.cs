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
    public class  EvaluacionEstudianteMapping : IEntityTypeConfiguration <EvaluacionEstudiante>
    {
        public void Configure(EntityTypeBuilder<EvaluacionEstudiante> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Estudiante)
                .WithMany(x => x.EvaluacionesEstudiante)
                .HasForeignKey(x => x.EstudianteId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.EvaluacionAula)
                .WithMany(x => x.EvaluacionesEstudiante)
                .HasForeignKey(x => x.EvaluacionAulaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("EvaluacionesEstudiante");
        }
    }
}
