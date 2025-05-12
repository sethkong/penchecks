using ATMApp.Data.Repositories;
using ATMApp.Domain;

namespace ATMApp.Data.Seeds
{
    public class AccountTypeSeed
    {
        private static readonly ITypeRepository<EntityKind> _repository = new TypeRepository<EntityKind>();
        public static void Up()
        {
            _repository.Add("Checking", "Checking", "The checking account");
            _repository.Add("Saving", "Saving", "The saving account");
        }

        public static void Down()
        {
            _repository.Remove("Checking");
            _repository.Remove("Saving");
        }
    }
}
