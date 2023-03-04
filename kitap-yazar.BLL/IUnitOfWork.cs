using kitap_yazar.BLL.Repositories.Abstract;

namespace kitap_yazar.BLL
{
    public interface IUnitOfWork:IDisposable
    {
        IAuthorRepository AuthorRepository{ get; }
        IBookRepository BookRepository { get; }

        int Complete();
    }
}
