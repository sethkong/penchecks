using ATMApp.Data.EntityConfigurations;
using ATMApp.Data.EntityConfigurations.BankAccount;
using ATMApp.Domain;
using ATMApp.Domain.BankAccount;
using Microsoft.EntityFrameworkCore;

namespace ATMApp.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly string _connectionString = string.Empty;

        public DbSet<EntityKind> EntityKinds { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DatabaseContext()
        {
            _connectionString = Environment.GetEnvironmentVariable("ATM_ConnectionStrings__SqlConnectionString")
                ?? "Host=localhost;Port=5432;Database=atmapp_dev;Username=postgres;Password=postgres";
        }

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(_connectionString))
                optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrWhiteSpace(_connectionString))
                modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfiguration(new EntityKindConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
