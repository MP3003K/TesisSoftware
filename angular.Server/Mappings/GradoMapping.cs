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
    public class GradoMapping : IEntityTypeConfiguration <Grado>
    {
        public void Configure(EntityTypeBuilder<Grado> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Nivel)
                .WithMany(x => x.Grados)
                .HasForeignKey(x => x.NivelId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Grados");
        }
    }
}
