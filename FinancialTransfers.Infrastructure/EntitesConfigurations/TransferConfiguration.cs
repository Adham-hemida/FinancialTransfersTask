using FinancialTransfers.Domain.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransfers.Infrastructure.EntitesConfigurations;
public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
	public void Configure(EntityTypeBuilder<Transfer> builder)
	{
		builder.HasIndex(t => t.Status);
		builder.HasIndex(t => new { t.FromAccountId, t.Status });
		builder.HasIndex(t => new { t.ToAccountId, t.Status });


		builder.Property(t => t.Amount).IsRequired().HasPrecision(18, 2);
		builder.Property(t => t.Fees).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);
		builder.Property(t => t.Currency).IsRequired().HasMaxLength(10);
		builder.Property(t => t.Status).IsRequired().HasMaxLength(50).HasDefaultValue(TransferStatus.Pending);
		builder.Property(t => t.Description).HasMaxLength(1500);

		builder.HasOne(t => t.FromAccount)     
		   .WithMany(a => a.FromTransfers)     
		   .HasForeignKey(t => t.FromAccountId);

		builder.HasOne(t => t.ToAccount)
			.WithMany(a => a.ToTransfers)
		  .HasForeignKey(t => t.ToAccountId);

	}
}
