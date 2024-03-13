using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class GradoEvaPsicologicaMapping : IEntityTypeConfiguration<GradoEvaPsicologica>
    {
        public void Configure(EntityTypeBuilder<GradoEvaPsicologica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.EvaluacionPsicologica)
                .WithMany(x => x.GradosEvaPsicologicas)
                .HasForeignKey(x => x.EvaPsiId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Grado)
                .WithMany(x => x.GradosEvaPsicologicas)
                .HasForeignKey(x => x.GradoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("GradosEvaPsicologicas");
        }
    }
}
