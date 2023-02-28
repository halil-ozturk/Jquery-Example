using kitap_yazar.BLL.DTOs.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kitap_yazar.BLL.Services.Yazar
{
    public interface IAuthorService
    {
        Task<List<AuthorInfoDto>> GetAuthors();
        AuthorInfoDto AddAuthor(AuthorInfoDto authorInfoDto);
        Task<IEnumerable<object>> TotalBookCount();
    }
}
