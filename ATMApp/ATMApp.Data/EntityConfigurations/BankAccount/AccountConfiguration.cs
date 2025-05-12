using ATMApp.Domain.BankAccount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATMApp.Data.EntityConfigurations.BankAccount
{
    public class AccountConfiguration : BaseConfiguration<Account>, IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_accounts_id");

            builder.Property(x => x.Name).HasColumnName("name")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(x => x.AccountNumber).HasColumnName("account_number")
             .HasMaxLength(12)
             .IsRequired();

            builder.Property(x => x.RoutingNumber).HasColumnName("routing_number")
             .HasMaxLength(12)
             .IsRequired();

            builder.Property(x => x.Balance).HasColumnName("balance")
            .IsRequired();

            builder.Property(x => x.AccountTypeId).HasColumnType("uuid")
            .HasColumnName("account_type_id");

            builder.HasIndex(x => new { x.AccountNumber, x.Name }).IsUnique()
              .HasDatabaseName("idx_accounts_name_account_number");

            BaseConfigure(builder);

            builder.ToTable("accounts");
        }
    }
}
