using FluentValidation;

namespace ATMApp.Domain.BankAccount
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.Balance).NotNull().WithMessage("Balance is quired");
            RuleFor(x => x.Amount).NotNull().WithMessage("Amount is required");
            RuleFor(x => x.AccountId).NotNull().WithMessage("Account ID is requied");
        }
    }
}
