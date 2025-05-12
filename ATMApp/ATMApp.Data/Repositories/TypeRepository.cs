using ATMApp.Domain;

namespace ATMApp.Data.Repositories
{
    public class TypeRepository<T> : ITypeRepository<T> where T : EntityKind, new()
    {
        public void Add(string name, string code, string description)
        {
            using var context = new DatabaseContext();
            if (!context.Set<T>().Any(x => x.Code == code))
            {
                context.Set<T>().Add(new T()
                {
                    Name = name,
                    Code = code,
                    Description = description
                });
                context.SaveChanges();
            }
        }

        public void Remove(string code)
        {
            using var context = new DatabaseContext();
            var entity = context.Set<T>()
                    .FirstOrDefault(x => x.Code == code);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
