namespace ATMApp.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? InsertedAt { get; set; }
        public DateTime? InactiveAt { get; set; }
        public Guid? InsertedBy { get; set; } = null;
        public Guid? UpdatedBy { get; set; } = null;
        public DateTime? DeletedAt { get; set; }
        public BaseEntity()
        {
            this.InsertedAt = DateTime.UtcNow;
        }
    }
}
