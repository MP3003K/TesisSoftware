using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Domain.Entities.Transaction>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Transaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Pix)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.PixId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Date)
            .HasDefaultValueSql("GETDATE()");

        builder.ToTable("Transactions");
    }
}