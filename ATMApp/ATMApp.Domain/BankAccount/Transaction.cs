namespace ATMApp.Domain.BankAccount
{
    public class Transaction : BaseEntity
    {
        public DateTime? PostingDate { get; set; }
        public Guid TransactionTypeId { get; set; }
        public EntityKind TransactionType { get; set; } = new EntityKind();
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public bool Reconcile { get; set; } = false;
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = new Account();
    }
}
