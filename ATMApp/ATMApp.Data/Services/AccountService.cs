using ATMApp.Domain;
using ATMApp.Domain.BankAccount;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ATMApp.Data.Repositories
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<IAccountService> _logger;
        private readonly IValidator<Account> _accountValidator;
        private readonly IValidator<Transaction> _transactionValidator;
        private readonly IValidator<EntityKind> _entityKindValidator;

        public AccountService(ILogger<IAccountService> logger,
            IValidator<Account> accountValidator,
            IValidator<Transaction> transactionValidator,
            IValidator<EntityKind> entityKindValidator)
        {
            _logger = logger;
            _accountValidator = accountValidator;
            _transactionValidator = transactionValidator;
            _entityKindValidator = entityKindValidator;
        }

        //public Task<ApiResponse<List<Account>>> GetAccounts(CancellationToken token = default(CancellationToken))
        //{

        //}
    }
}
