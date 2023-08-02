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
    public class EscalaMapping : IEntityTypeConfiguration<Escala>
    {
        public void Configure(EntityTypeBuilder<Escala> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Dimension)
                .WithMany(x => x.Escalas)
                .HasForeignKey(x => x.DimensionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Escalas");
        }
    }
}
