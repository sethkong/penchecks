﻿using Newtonsoft.Json;

namespace ATMApp.Domain.BankAccount
{
    public class Transaction : BaseEntity
    {
        public DateTime? PostingDate { get; set; }
        public Guid TransactionTypeId { get; set; }
        public EntityKind TransactionType { get; set; } = new EntityKind();
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public bool Reconcile { get; set; } = false;
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = new Account();
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
