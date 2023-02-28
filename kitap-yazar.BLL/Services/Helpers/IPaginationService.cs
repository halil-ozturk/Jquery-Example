using kitap_yazar.BLL.DTOs.Book;
using kitap_yazar.BLL.DTOs.Helper;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace kitap_yazar.BLL.Services.Helpers
{
    public interface IPaginationService<T> where T : class
    {
        public Task<PaginationResponseDto<T>> GetPaginationResponses(int bc, int pn, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>> predicate = null);
        public int PaginationCount(int it);
    }
}
