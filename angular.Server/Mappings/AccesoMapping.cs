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
    public class AccesoMapping: IEntityTypeConfiguration<Acceso>
    {
        public void Configure(EntityTypeBuilder<Acceso> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Accesos");
        }
    }
}
