using kitap_yazar.BLL.Abstract;
using kitap_yazar.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace kitap_yazar.BLL.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public KitapYazarDatabaseContext _db;
        private DbSet<TEntity> _dbSet;

        public Repository(KitapYazarDatabaseContext context)
        {
            _db = context;
            _dbSet = _db.Set<TEntity>();
        }

        public bool Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return true;
        }

        public IEnumerable<TEntity> GetAll(string entity)
        {
            return _dbSet.Include(entity);
        }

        public IEnumerable<TEntity> GetByCount(string entity, int count)
        {
            return _dbSet.Include(entity).Take(count);
        }

        public TEntity GetByID(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
