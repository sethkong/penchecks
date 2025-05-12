using ATMApp.Domain;

namespace ATMApp.Data.Repositories
{
    public interface ITypeRepository<T> where T : EntityKind, new()
    {
        void Add(string name, string code, string description);
        void Remove(string code);
    }
}