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
    public class IndicadorPsicologicoMapping : IEntityTypeConfiguration <IndicadorPsicologico>
    {
        public void Configure(EntityTypeBuilder<IndicadorPsicologico> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.EscalaPsicologica)
                .WithMany(x => x.IndicadoresPsicologicos)
                .HasForeignKey(x => x.EscalaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("IndicadoresPsicologicos");
        }
    }
}
