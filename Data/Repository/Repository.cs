using Data.Context;
using Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyContext _context;

        public Repository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Buscar(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public IList<T> Get()
        {
            return _context.Set<T>()
                .AsNoTracking()
                .ToList();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

    }
}
