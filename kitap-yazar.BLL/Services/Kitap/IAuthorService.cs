using kitap_yazar.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kitap_yazar.BLL.Services.Kitap
{
    public interface IAuthorService
    {
        Task<List<AuthorInfoDto>> GetAuthors();
        AuthorInfoDto AddAuthor(AuthorInfoDto authorInfoDto);
    }
}
