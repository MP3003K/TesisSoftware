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
    public class  EvaluacionPsicologicaEstudianteMapping : IEntityTypeConfiguration <EvaluacionPsicologicaEstudiante>
    {
        public void Configure(EntityTypeBuilder<EvaluacionPsicologicaEstudiante> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Estudiante)
                .WithMany(x => x.EvaluacionesEstudiante)
                .HasForeignKey(x => x.EstudianteId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.EvaluacionAula)
                .WithMany(x => x.EvaluacionesPsicologicasEstudiante)
                .HasForeignKey(x => x.EvaluacionAulaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.FechaFin).IsRequired(false);
            builder.Property(x => x.FechaInicio).IsRequired(false);

            builder.Property(x => x.Estado).HasComment("(No inicio= N, En proceso = P, Finalizo= F)");

            builder.ToTable("EvaluacionesPsicologicasEstudiante");
        }
    }
}
