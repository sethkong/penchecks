using Newtonsoft.Json;

namespace ATMApp.Domain
{
    public class EntityKind : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? ParentId { get; set; } = null;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
