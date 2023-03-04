using kitap_yazar.BLL.Repositories.Abstract;
using kitap_yazar.DOMAIN.Models;

namespace kitap_yazar.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private KitapYazarDatabaseContext _db;
        public UnitOfWork(KitapYazarDatabaseContext db, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _db = db;
            AuthorRepository = authorRepository;
            BookRepository = bookRepository;
        }
        public IAuthorRepository AuthorRepository { get; private set; }

        public IBookRepository BookRepository { get; private set; }

        public int Complete()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
