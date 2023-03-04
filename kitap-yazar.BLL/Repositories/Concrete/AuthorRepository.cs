using kitap_yazar.BLL.Concrete;
using kitap_yazar.BLL.Repositories.Abstract;
using kitap_yazar.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace kitap_yazar.BLL.Repositories.Concrete
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(KitapYazarDatabaseContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        public async Task<IEnumerable<object>> TotalBookCount()
        {
            var authorBookCount = _db.Books
                .GroupBy(x => x.AuthorID)
                .Select(y => new { BookCount = y.Count(), AuthorID = y.Key })
                .GroupBy(k => k.BookCount)
                .Select(z => new { BookCount = z.Key, AuthorCount = z.Count() })
                .ToList();

            var authorCountWithNoBook = _db.Authors.Include(x => x.Book)
                .Where(x => x.Book.Count == default)
                .Count();

            authorBookCount.Add(new { BookCount = 0, AuthorCount = authorCountWithNoBook });

            return authorBookCount;
        }
        public KitapYazarDatabaseContext KitapYazarDatabaseContext { get { return _db as KitapYazarDatabaseContext; } }

    }
}
