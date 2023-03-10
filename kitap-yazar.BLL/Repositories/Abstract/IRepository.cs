using System.Linq.Expressions;

namespace kitap_yazar.BLL.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(int id);
        bool Add(TEntity entity);
        IEnumerable<TEntity> GetAll(string entity);
        IQueryable<TEntity> GetByCount(string entity , int count);

    }
}
