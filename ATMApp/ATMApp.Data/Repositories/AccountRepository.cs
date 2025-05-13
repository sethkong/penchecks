using ATMApp.Domain;
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

        public async Task<Account> OpenBankAccount(Account account, CancellationToken token = default)
        {
            _logger.Log(LogLevel.Information, "Opens a new bank account: {account}", account.ToString());
            using var context = _options != null ?
                  new DatabaseContext(_options) : new DatabaseContext();
            context.Accounts.Entry(account).State = EntityState.Added;
            await context.SaveChangesAsync();

            var newAccount = await context.Accounts.Where(x => x.Id == account.Id)
                .Include(x => x.AccountType)
                .Include(x => x.Transactions).ThenInclude(x => x.TransactionType)
                .FirstOrDefaultAsync() ?? throw new Exception("Failed to fetch the newly created account");

            return await Task.FromResult(newAccount);
        }

        public async Task<Transaction> Deposit(Guid accountId, decimal amount, CancellationToken token = default)
        {
            _logger.Log(LogLevel.Information, "Deposits {amount} into {accountId}", amount, accountId);
            using var context = _options != null ?
                   new DatabaseContext(_options) : new DatabaseContext();
            return await ExecuteTransaction(context, TransactionAction.Deposit, accountId, amount, token);
        }

        public async Task<List<Account>> GetAccounts(CancellationToken token = default(CancellationToken))
        {
            _logger.Log(LogLevel.Information, "Fetches all accounts");
            using var context = _options != null ?
                   new DatabaseContext(_options) : new DatabaseContext();
            return await context.Accounts
                .Include(x => x.AccountType)
                .ToListAsync(token);
        }

        public async Task<List<Transaction>> GetTransactions(Guid accountId, CancellationToken token = default)
        {
            _logger.Log(LogLevel.Information, "Fetches all transactions by account ID: {accountId}", accountId);
            using var context = _options != null ?
                  new DatabaseContext(_options) : new DatabaseContext();
            return await context.Transactions.Where(x => x.AccountId == accountId)
                .Include(x => x.TransactionType)
                .Include(x => x.Account)
                .ToListAsync(token);
        }

        public async Task<Transaction> Withdraw(Guid accountId, decimal amount, CancellationToken token = default)
        {
            _logger.Log(LogLevel.Information, "Withdraws {amount} from {accountId}", amount, accountId);
            using var context = _options != null ?
                   new DatabaseContext(_options) : new DatabaseContext();
            return await ExecuteTransaction(context, TransactionAction.Withdraw, accountId, amount, token);
        }

        private async Task<Transaction> ExecuteTransaction(
            DatabaseContext context,
            TransactionAction action,
            Guid accountId,
            decimal amount,
            CancellationToken token = default)
        {
            using var _ = await context.Database.BeginTransactionAsync();
            var account = await context.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync(token);
            var transaction = new Transaction();
            if (account == null)
            {
                _logger.Log(LogLevel.Error, "Cannot determine account");
                await context.Database.RollbackTransactionAsync(token);
                return await Task.FromResult(transaction);
            }
            switch (action)
            {
                case TransactionAction.Deposit:
                    account.Balance += amount;
                    break;
                case TransactionAction.Withdraw:
                    account.Balance -= amount;
                    break;
                default:
                    throw new Exception("Unknown transaction action");
            }

            var transactionType = await context.EntityKinds
                .Where(x => x.Code == TransactionType.Credit.ToString())
                .FirstOrDefaultAsync(token);

            if (transactionType == null)
            {
                _logger.Log(LogLevel.Error, "Cannot determine transaction type");
                await context.Database.RollbackTransactionAsync(token);
                return await Task.FromResult(transaction);
            }

            transaction.TransactionTypeId = transactionType.Id;
            transaction.AccountId = accountId;
            transaction.Balance = account.Balance;
            transaction.Amount = amount;
            transaction.PostingDate = DateTime.UtcNow;

            context.Transactions.Entry(transaction).State = EntityState.Added;
            await context.SaveChangesAsync(token);
            await context.Database.CommitTransactionAsync(token);

            var newTransaction = await context.Transactions.Where(x => x.Id == transaction.Id)
                .Include(x => x.TransactionType)
                .FirstOrDefaultAsync(token) ?? throw new Exception("Failed to fetch the newly created transaction");

            return await Task.FromResult(newTransaction);
        }

        public async Task<List<EntityKind>> GetEntityKinds(string? code = null, CancellationToken token = default)
        {
            _logger.Log(LogLevel.Information, "Fetches all entity kinds by optional code: {code}", code);
            using var context = _options != null ?
                  new DatabaseContext(_options) : new DatabaseContext();
            return string.IsNullOrEmpty(code) ? await context.EntityKinds.ToListAsync(token)
                : await context.EntityKinds.Where(x => x.Code == code).ToListAsync(token);
        }

        public async Task<bool> CanWithdraw(Guid accountId, decimal amount, CancellationToken token = default)
        {
            _logger.Log(LogLevel.Information, "Checks if {amount} can be withdrawn from {accountId}", amount, accountId);
            using var context = _options != null ?
                   new DatabaseContext(_options) : new DatabaseContext();
            var account = await context.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync(token);
            if (account == null)
            {
                _logger.Log(LogLevel.Error, "Cannot determine account");
                return await Task.FromResult(false);
            }
            return await Task.FromResult(account.Balance >= amount);
        }
    }
}
