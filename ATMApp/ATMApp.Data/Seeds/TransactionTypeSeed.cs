using ATMApp.Data.Repositories;
using ATMApp.Domain;

namespace ATMApp.Data.Seeds
{
    public class TransactionTypeSeed
    {
        private static readonly ITypeRepository<EntityKind> _repository = new TypeRepository<EntityKind>();
        public static void Up()
        {
            _repository.Add("TransactionType", "Transaction Type", "The transaction type");
            var transactionType = _repository.Get("AccountType");
            if (transactionType != null)
            {
                _repository.Add("Debit", "Debit", "The debit transaction", transactionType.Id);
                _repository.Add("Credit", "Credit", "The credit transaction", transactionType.Id);
                _repository.Add("Transfer", "Transfer", "The transfer transaction", transactionType.Id);
            }
        }

        public static void Down()
        {
            _repository.Remove("Debit");
            _repository.Remove("Credit");
            _repository.Remove("Transfer");
        }
    }
}
