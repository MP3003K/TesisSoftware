using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class PreguntaPsicologicaMapping: IEntityTypeConfiguration<PreguntaPsicologica>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PreguntaPsicologica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.IndicadorPsicologico)
                .WithMany(x => x.PreguntasPsicologicas)
                .HasForeignKey(x => x.IndicadorPsicologicoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.NPregunta).IsRequired(false);

            builder.ToTable("PreguntasPsicologicas");
        }
    }
}
