using ATMApp.Data.Repositories;
using ATMApp.Domain;

namespace ATMApp.Data.Seeds
{
    public class AccountTypeSeed
    {
        private static readonly ITypeRepository<EntityKind> _repository = new TypeRepository<EntityKind>();

        public static void Up()
        {
            _repository.Add("AccountType", "Account Type", "The account type");
            var accountType = _repository.Get("AccountType");
            if (accountType != null)
            {
                _repository.Add("Checking", "Checking", "The checking account", accountType.Id);
                _repository.Add("Saving", "Saving", "The saving account", accountType.Id);
            }
        }

        public static void Down()
        {
            _repository.Remove("Checking");
            _repository.Remove("Saving");
        }
    }
}
