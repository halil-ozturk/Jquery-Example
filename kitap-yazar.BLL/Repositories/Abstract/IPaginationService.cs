using kitap_yazar.BLL.DTOs.Helper;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace kitap_yazar.BLL.Repositories.Abstract
{
    public interface IPaginationService<T> where T : class
    {
        public Task<PaginationResponseDto<T>> GetPaginationResponses(int bc, int pn, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>> predicate = null);
        public int PaginationCount(int it);
    }
}
