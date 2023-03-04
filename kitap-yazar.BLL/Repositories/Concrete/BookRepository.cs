using kitap_yazar.BLL.Concrete;
using kitap_yazar.BLL.DTOs.Book;
using kitap_yazar.BLL.Repositories.Abstract;
using kitap_yazar.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace kitap_yazar.BLL.Repositories.Concrete
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        protected readonly DbSet<Book> _dbSet;
        public BookRepository(KitapYazarDatabaseContext dbContext) : base(dbContext)
        {
            _db = dbContext;
            _dbSet = _db.Set<Book>();
        }

        public async Task<List<BookInfoDto>> SearchBooks(string? sb = null, string? sa = null)
        {
            List<BookInfoDto> books = await _db.Books.Include(x => x.Author)
          .Select(x => new BookInfoDto() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name })
          .Where(x => x.Name.Contains(sb) || x.AuthorName.Contains(sa))
          .ToListAsync();

            return books;
        }

        public KitapYazarDatabaseContext KitapYazarDatabaseContext { get { return _db; } }
    }
}
