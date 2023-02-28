using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using kitap_yazar.BLL.DTOs.Book;
using kitap_yazar.BLL.DTOs.Helper;
using kitap_yazar.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace kitap_yazar.BLL.Services.Helpers
{
    public class PaginationService<T> : IPaginationService<T> where T : class
    {
        private readonly KitapYazarDatabaseContext _db;
        protected readonly DbSet<T> _dbSet;

        public PaginationService(KitapYazarDatabaseContext databaseContext)
        {
            _db = databaseContext;
            _dbSet = _db.Set<T>();
        }

        public async Task<PaginationResponseDto<T>> GetPaginationResponses(int ic, int pn, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var gelecekKitap = (pn - 1) * ic;
            if (gelecekKitap == 0)
            {
                var items = await query
            .Select(x => x).Take(ic)
            .ToListAsync();


                return new PaginationResponseDto<T> { ItemCount = items.Count, Items = items, PageCount = items.Count / ic };
            }
            else
            {
                var items = await query
                                 .Select(x => x).Skip(gelecekKitap).Take(ic)
                                 .ToListAsync();

                return new PaginationResponseDto<T> { ItemCount = items.Count, Items = items, PageCount = items.Count / ic };

                //List<PaginationResponseDto<T>> items = await query.Select(x => new PaginationResponseDto<T>() { ItemCount = query.Count(), Items = query.ToList(), PageCount = query.Count() / ic }).Skip(gelecekKitap).Take(ic).ToListAsync();

                //return items;
            }
        }

        public int PaginationCount(int it)
        {
            IQueryable<T> query = _dbSet;

            var PageCount = query.Count() / it + 1;

            return PageCount;
        }
    }

}
