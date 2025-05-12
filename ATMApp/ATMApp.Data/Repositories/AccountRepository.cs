using ATMApp.Domain.BankAccount;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ATMApp.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger<IAccountService> _logger;
        private readonly DbContextOptions<DatabaseContext>? _options;

        public AccountRepository(ILogger<IAccountService> logger,
            DbContextOptions<DatabaseContext>? options = null)
        {
            _logger = logger;
            _options = options;
        }

        public async Task<Transaction> Deposit(Guid accountId, decimal amount, CancellationToken token = default)
        {
            _logger.Log(LogLevel.Information, "Fetches all accounts");
            using var context = _options != null ?
                   new DatabaseContext(_options) : new DatabaseContext();
            using var _ = await context.Database.BeginTransactionAsync();
            var account = await context.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if (account != null)
            {
                account.Balance -= amount;   
            }
            throw new NotImplementedException();
        }

        public async Task<List<Account>> GetAccounts(CancellationToken token = default(CancellationToken))
        {
            _logger.Log(LogLevel.Information, "Fetches all accounts");
            using var context = _options != null ?
                   new DatabaseContext(_options) : new DatabaseContext();
            return await context.Accounts.ToListAsync(token);
        }

        public async Task<List<Transaction>> GetTransactions(Guid accountId, CancellationToken token = default)
        {
            _logger.Log(LogLevel.Information, "Fetches all transactions by account ID");
            using var context = _options != null ?
                  new DatabaseContext(_options) : new DatabaseContext();
            return await context.Transactions.Where(x => x.AccountId == accountId).ToListAsync(token);
        }

        public async Task<Transaction> Withdraw(Guid accountId, decimal amount, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
