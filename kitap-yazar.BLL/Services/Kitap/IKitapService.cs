using kitap_yazar.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kitap_yazar.BLL.Services.Kitap
{
    public interface IKitapService
    {
        Task<List<BookInfoDto2>> GetBooks(int bc, int pn);
        Task<List<BookInfoDto2>> SearchBooks(string? sb = null, string? sa = null);
        BookInfoDto AddBook(BookInfoDto bookInfoDto);

    }
}
