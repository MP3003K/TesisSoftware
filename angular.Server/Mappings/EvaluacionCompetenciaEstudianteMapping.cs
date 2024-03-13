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
    public class EvaluacionCompetenciaEstudianteMapping: IEntityTypeConfiguration<EvaluacionCompetenciaEstudiante>
    {
        public void Configure(EntityTypeBuilder<EvaluacionCompetenciaEstudiante> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CompetenciaAcademica)
                .WithMany(x => x.EvaluacionesCompetenciasEstudiante)
                .HasForeignKey(x => x.CompetenciaAcademicaId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.CompetenciaAcademica)
                .WithMany(x => x.EvaluacionesCompetenciasEstudiante)
                .HasForeignKey(x => x.CompetenciaAcademicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Estudiante)
                .WithMany(x => x.EvaluacionesCompetenciasEstudiante)
                .HasForeignKey(x => x.EstudianteId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("EvaluacionesCompetenciasEstudiante");
        }
    }
}
