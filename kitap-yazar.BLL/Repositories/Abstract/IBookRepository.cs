using kitap_yazar.BLL.Abstract;
using kitap_yazar.BLL.DTOs.Book;
using kitap_yazar.DOMAIN.Models;

namespace kitap_yazar.BLL.Repositories.Abstract
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<BookInfoDto>> SearchBooks(string? sb = null, string? sa = null);
    }
}
