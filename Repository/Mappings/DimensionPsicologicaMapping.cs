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
    public class DimensionPsicologicaMapping : IEntityTypeConfiguration<DimensionPsicologica>
    {
        public void Configure(EntityTypeBuilder<DimensionPsicologica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.EvaluacionPsicologica)
                .WithMany(x => x.DimensionesPsicologicas)
                .HasForeignKey(x => x.EvaluacionPsicologicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("DimensionesPsicologicas");
        }
    }
}
