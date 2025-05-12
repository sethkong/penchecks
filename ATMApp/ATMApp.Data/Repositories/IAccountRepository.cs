using ATMApp.Domain.BankAccount;

namespace ATMApp.Data.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccounts(CancellationToken token = default(CancellationToken));
        Task<List<Transaction>> GetTransactions(Guid accountId, CancellationToken token = default(CancellationToken));
        Task<Transaction> Withdraw(Guid accountId, decimal amount, CancellationToken token = default(CancellationToken));
        Task<Transaction> Deposit(Guid accountId, decimal amount, CancellationToken token = default(CancellationToken));
    }
}
