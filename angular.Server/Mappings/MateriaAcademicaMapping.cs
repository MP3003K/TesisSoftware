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
    public class MateriaAcademicaMapping: IEntityTypeConfiguration<MateriaAcademica>
    {
        public void Configure(EntityTypeBuilder<MateriaAcademica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.CompetenciasAcademicas)
                .WithOne(x => x.MateriaAcademica)
                .HasForeignKey(x => x.MateriaAcademicaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("MateriasAcademicas");
        }
    }
}
