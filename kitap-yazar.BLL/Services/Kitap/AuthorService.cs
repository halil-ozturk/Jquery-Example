using kitap_yazar.BLL.DTOs;
using kitap_yazar.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace kitap_yazar.BLL.Services.Kitap
{
    public class AuthorService : IAuthorService
    {
        public AuthorService(KitapYazarDatabaseContext context)
        {
            _db = context;
        }
        private readonly KitapYazarDatabaseContext _db;
        public async Task<List<AuthorInfoDto>> GetAuthors()
        {
            List<AuthorInfoDto> authors = _db.Authors
                .Select(x => new AuthorInfoDto() { AuthorID = x.AuthorID, AuthorName = x.Name, TotalBook = x.Book.Count() })
                .ToList();

            return authors;
        }

        public AuthorInfoDto AddAuthor(AuthorInfoDto authorInfoDto)
        {
            var author = new Author(authorInfoDto.AuthorName);

            _db.Authors.Add(author);
            _db.SaveChanges();

            return authorInfoDto;
        }
    }
}
