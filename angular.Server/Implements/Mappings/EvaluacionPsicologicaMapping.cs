using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implements.Mappings
{
    public class EvaluacionPsicologicaMapping : IEntityTypeConfiguration<EvaluacionPsicologica>
    {
        public void Configure(EntityTypeBuilder<EvaluacionPsicologica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("EvaluacionesPsicologicas");
        }
    }
}
