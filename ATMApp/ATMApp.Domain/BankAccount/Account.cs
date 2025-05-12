using Newtonsoft.Json;

namespace ATMApp.Domain.BankAccount
{
    public class Account : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string RoutingNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public Guid AccountTypeId { get; set; }
        public EntityKind AccountType { get; set; } = new EntityKind();
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
