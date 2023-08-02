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
    public class PruebaGradoMapping : IEntityTypeConfiguration <PruebaGrado>
    {
        public void Configure(EntityTypeBuilder<PruebaGrado> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Grado)
                .WithMany(x => x.PruebasGrado)
                .HasForeignKey(x => x.GradoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PruebaPsicologica)
                .WithMany(x => x.PruebasGrado)
                .HasForeignKey(x => x.PruebaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("PruebasGrado");
        }
    }
}
