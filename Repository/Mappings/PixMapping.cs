using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings;

public class PixMapping : IEntityTypeConfiguration<Pix>
{
    public void Configure(EntityTypeBuilder<Pix> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Bank)
            .WithMany(x => x.Pixes)
            .HasForeignKey(x => x.BankId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Pixes");
    }
}