using ATMApp.Domain;
using ATMApp.Domain.BankAccount;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ATMApp.Data.Repositories
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<IAccountService> _logger;
        private readonly IValidator<Account> _accountValidator;
        private readonly IValidator<Transaction> _transactionValidator;
        private readonly IValidator<EntityKind> _entityKindValidator;
        private readonly IAccountRepository _accountRepository;

        public AccountService(ILogger<IAccountService> logger,
            IValidator<Account> accountValidator,
            IValidator<Transaction> transactionValidator,
            IValidator<EntityKind> entityKindValidator,
            IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountValidator = accountValidator;
            _transactionValidator = transactionValidator;
            _entityKindValidator = entityKindValidator;
            _accountRepository = accountRepository;
        }

        public async Task<ApiResponse<Transaction>> Deposit(Guid accountId, decimal amount, CancellationToken token = default)
        {
            var apiResponse = new ApiResponse<Transaction>();
            try
            {
                _logger.LogInformation("Feches all accounts");
                var transaction = await _accountRepository.Deposit(accountId, amount, token);
                apiResponse.IsSuccessful = true;
                apiResponse.Message = "Deposited successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.Data = transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse.IsSuccessful = false;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Data = null;
            }
            return await Task.FromResult(apiResponse);
        }

        public async Task<ApiResponse<List<Account>>> GetAccounts(CancellationToken token = default(CancellationToken))
        {
            var apiResponse = new ApiResponse<List<Account>>();
            try
            {
                _logger.LogInformation("Feches all accounts");
                var accounts = await _accountRepository.GetAccounts(token);
                apiResponse.IsSuccessful = true;
                apiResponse.Message = "Gets accounts successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.Data = accounts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse.IsSuccessful = false;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Data = null;
            }
            return await Task.FromResult(apiResponse);
        }

        public async Task<ApiResponse<List<EntityKind>>> GetEntityKinds(string? code = null, CancellationToken token = default)
        {
            var apiResponse = new ApiResponse<List<EntityKind>>();
            try
            {
                _logger.LogInformation("Feches entity kinds by optional code: {code}", code);
                var kinds = await _accountRepository.GetEntityKinds(code, token);
                apiResponse.IsSuccessful = true;
                apiResponse.Message = "Gets entity kinds successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.Data = kinds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse.IsSuccessful = false;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Data = null;
            }
            return await Task.FromResult(apiResponse);
        }

        public async Task<ApiResponse<List<Transaction>>> GetTransactions(Guid accountId, CancellationToken token = default)
        {
            var apiResponse = new ApiResponse<List<Transaction>>();
            try
            {
                _logger.LogInformation("Feches all transactions");
                var transactions = await _accountRepository.GetTransactions(accountId, token);
                apiResponse.IsSuccessful = true;
                apiResponse.Message = "Gets transactions successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.Data = transactions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse.IsSuccessful = false;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Data = null;
            }
            return await Task.FromResult(apiResponse);
        }

        public async Task<ApiResponse<Account>> OpenBankAccount(Account account, CancellationToken token = default)
        {
            var apiResponse = new ApiResponse<Account>();
            try
            {
                _logger.LogInformation("Open a new bank account");
                var accountValidationResult = await _accountValidator.ValidateAsync(account);
                if (!accountValidationResult.IsValid)
                {
                    _logger.LogError("Input validation errors. Errors = {0}", string.Join(", ", apiResponse.Errors));
                    apiResponse.Errors = accountValidationResult.Errors.Select(error => error.ErrorMessage).ToList();
                    apiResponse.IsSuccessful = false;
                    apiResponse.Message = "Missing required fields";
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return apiResponse;
                }
                await _accountRepository.OpenBankAccount(account, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse.IsSuccessful = false;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Data = null;
            }
            return await Task.FromResult(apiResponse);
        }

        public async Task<ApiResponse<Transaction>> Withdraw(Guid accountId, decimal amount, CancellationToken token = default)
        {
            var apiResponse = new ApiResponse<Transaction>();
            try
            {
                _logger.LogInformation("Feches all accounts");
                var transaction = await _accountRepository.Withdraw(accountId, amount, token);
                apiResponse.IsSuccessful = true;
                apiResponse.Message = "Withdrew successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                apiResponse.Data = transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse.IsSuccessful = false;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                apiResponse.Data = null;
            }
            return await Task.FromResult(apiResponse);
        }
    }
}
