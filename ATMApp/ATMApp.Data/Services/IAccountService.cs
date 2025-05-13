using ATMApp.Domain;
using ATMApp.Domain.BankAccount;

namespace ATMApp.Data.Repositories
{
    public interface IAccountService
    {
        Task<ApiResponse<List<Account>>> GetAccounts(CancellationToken token = default(CancellationToken));
        Task<ApiResponse<Account>> OpenBankAccount(Account account, CancellationToken token = default(CancellationToken));
        Task<ApiResponse<List<Transaction>>> GetTransactions(Guid accountId, CancellationToken token = default(CancellationToken));
        Task<ApiResponse<Transaction>> Withdraw(Guid accountId, decimal amount, CancellationToken token = default(CancellationToken));
        Task<ApiResponse<Transaction>> Deposit(Guid accountId, decimal amount, CancellationToken token = default(CancellationToken));
        Task<ApiResponse<List<EntityKind>>> GetEntityKinds(string? code = null, CancellationToken token = default(CancellationToken));
        Task<ApiResponse<List<Transaction>?>> Transfer(TransferRequest request, CancellationToken token = default);
    }
}
