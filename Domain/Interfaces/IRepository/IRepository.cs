using System.Linq.Expressions;

namespace Domain.Interfaces.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Buscar(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        IList<T> Get();
        T? GetById(int id);
        void Update(T entity);
    }
}
