using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransfers.Infrastructure.EntitesConfigurations;
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
	public void Configure(EntityTypeBuilder<Account> builder)
	{
		builder.HasIndex(a => a.Type);
		builder.HasIndex(a => a.Name	).IsUnique();

		builder.Property(a => a.Name).IsRequired().HasMaxLength(250);
		builder.Property(a => a.Type).IsRequired().HasMaxLength(100);
		builder.Property(a => a.Currency).IsRequired().HasMaxLength(10);
		builder.Property(a => a.Balance).IsRequired().HasPrecision(18, 2);



		builder.HasMany(a => a.FromTransfers)  
			.WithOne(t => t.FromAccount)      
			.HasForeignKey(t => t.FromAccountId);


		builder.HasMany(a => a.ToTransfers)
		  .WithOne(t => t.ToAccount)
		  .HasForeignKey(t => t.ToAccountId);
	}
}
