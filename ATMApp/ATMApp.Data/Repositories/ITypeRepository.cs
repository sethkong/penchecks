using ATMApp.Domain;

namespace ATMApp.Data.Repositories
{
    public interface ITypeRepository<T> where T : EntityKind, new()
    {
        void Add(string code, string name, string description, Guid? parentId = null);
        void Remove(string code);
        T? Get(string code);
    }
}