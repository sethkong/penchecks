using FluentValidation;

namespace ATMApp.Domain.BankAccount
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Account name is required");
            RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("Account number is required");
            RuleFor(x => x.RoutingNumber).NotEmpty().WithMessage("Routing number is required");
            RuleFor(x => x.AccountTypeId).NotEmpty().WithMessage("Account type ID is required");
        }
    }
}
