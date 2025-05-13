using ATMApp.Domain.BankAccount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATMApp.Data.EntityConfigurations.BankAccount
{
    internal class TransactionConfiguration : BaseConfiguration<Transaction>, IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_transactions_id");

            builder.Property(x => x.PostingDate)
              .HasColumnName("posting_date")
              .HasColumnType("timestamptz");

            builder.Property(x => x.TransactionTypeId).HasColumnType("uuid")
              .HasColumnName("transaction_type_id");

            builder.Property(x => x.Amount).HasColumnType("decimal(12,4)")
              .HasColumnName("amount");

            builder.Property(x => x.Description).HasColumnName("description")
             .HasMaxLength(255)
             .IsRequired(false);

            builder.Property(x => x.Balance).HasColumnType("decimal(12,4)")
              .HasColumnName("balance");

            builder.Property(x => x.Reconcile)
              .HasColumnName("reconcile")
              .HasDefaultValue(false);

            builder.Property(x => x.AccountId).HasColumnType("uuid")
              .HasColumnName("account_id");

            BaseConfigure(builder);

            builder.ToTable("transactions");
        }
    }
}
