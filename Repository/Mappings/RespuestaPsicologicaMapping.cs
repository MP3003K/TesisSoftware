using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class RespuestaPsicologicaMapping: IEntityTypeConfiguration<RespuestaPsicologica>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RespuestaPsicologica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.PreguntaPsicologica)
                .WithMany(x => x.RespuestasPsicologicas)
                .HasForeignKey(x => x.PreguntaPsicologicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.EvaluacionPsicologicaEstudiante)
                .WithMany(x => x.RespuestasPsicologicas)
                .HasForeignKey(x => x.EvaPsiEstId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("RespuestasPsicologicas");
        }
    }
}
