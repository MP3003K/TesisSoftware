using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings
{
    public class AulaMapping : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Tutor)
                .WithMany(x => x.Aulas)
                .HasForeignKey(x => x.TutorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Escuela)
                .WithMany(x => x.Aulas)
                .HasForeignKey(x => x.EscuelaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Aulas");
        }
    }
}
