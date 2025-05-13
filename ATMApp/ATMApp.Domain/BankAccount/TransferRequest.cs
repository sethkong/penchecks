using Newtonsoft.Json;

namespace ATMApp.Domain.BankAccount
{
    public class TransferRequest
    {
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransferAt { get; set; }
        public string? Notes { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
