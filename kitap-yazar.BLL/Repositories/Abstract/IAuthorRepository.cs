using kitap_yazar.BLL.Abstract;
using kitap_yazar.DOMAIN.Models;

namespace kitap_yazar.BLL.Repositories.Abstract
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<object>> TotalBookCount();
    }
}
