using ATMApp.Data.Repositories;
using ATMApp.Domain.BankAccount;
using Microsoft.AspNetCore.Mvc;

namespace ATMApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountService _accountService;

        public AccountsController(ILogger<AccountsController> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet("GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            _logger.LogInformation("Gets accounts");
            var source = new CancellationTokenSource();
            var accounts = await _accountService.GetAccounts(source.Token);
            return new JsonResult(accounts);
        }

        [HttpGet("GetTransactions")]
        public async Task<IActionResult> GetTransactions(Guid accountId)
        {
            _logger.LogInformation("Gets transactions");
            var source = new CancellationTokenSource();
            var transactions = await _accountService.GetTransactions(accountId, source.Token);
            return new JsonResult(transactions);
        }

        [HttpGet("GetTypes")]
        public async Task<IActionResult> GetTypes(string? code)
        {
            _logger.LogInformation("Gets entity kinds by optional code");
            var source = new CancellationTokenSource();
            var kinds = await _accountService.GetEntityKinds(code, source.Token);
            return new JsonResult(kinds);
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(Guid accountId, decimal amount)
        {
            _logger.LogInformation("Deposits {amount} into {accountId} account", amount, accountId);
            var source = new CancellationTokenSource();
            var transaction = await _accountService.Deposit(accountId, amount, source.Token);
            return new JsonResult(transaction);
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw(Guid accountId, decimal amount)
        {
            _logger.LogInformation("Withdraws {amount} from {accountId} account", amount, accountId);
            var source = new CancellationTokenSource();
            var transaction = await _accountService.Withdraw(accountId, amount, source.Token);
            return new JsonResult(transaction);
        }

        [HttpPost("OpenAccount")]
        public async Task<IActionResult> OpenAccount([FromBody] Account account)
        {
            _logger.LogInformation("Opens a new bank account: {account}", account.ToString());
            var source = new CancellationTokenSource();
            var newAccount = await _accountService.OpenBankAccount(account, source.Token);
            return new JsonResult(newAccount);
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            _logger.LogInformation("Transfer request: {0}", request.ToString());
            var source = new CancellationTokenSource();
            var transactions = await _accountService.Transfer(request, source.Token);
            return new JsonResult(transactions);
        }
    }
}
