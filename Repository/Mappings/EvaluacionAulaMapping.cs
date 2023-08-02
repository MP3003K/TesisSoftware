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
    public class EvaluacionAulaMapping : IEntityTypeConfiguration <EvaluacionAula>
    {
        public void Configure(EntityTypeBuilder<EvaluacionAula> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Aula)
                .WithMany(x => x.EvaluacionesAula)
                .HasForeignKey(x => x.AulaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Unidad)
                .WithMany(x => x.EvaluacionesAula)
                .HasForeignKey(x => x.UnidadId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PruebaGrado)
                .WithMany(x => x.EvaluacionesAula)
                .HasForeignKey(x => x.PruebaGradoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("EvaluacionesAula");
        }
    }
}
