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
    public class IndicadorMapping : IEntityTypeConfiguration <Indicador>
    {
        public void Configure(EntityTypeBuilder<Indicador> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Escala)
                .WithMany(x => x.Indicadores)
                .HasForeignKey(x => x.EscalaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.EvaluacionEstudiante)
                .WithMany(x => x.Indicadores)
                .HasForeignKey(x => x.EvaEstudianteId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Indicadores");
        }
    }
}
