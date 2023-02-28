using kitap_yazar.BLL.DTOs.Author;
using kitap_yazar.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace kitap_yazar.BLL.Services.Yazar
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

        //public async Task<List<AuthorInfoDto>> TotalBookCount()
        //{
        //    List<AuthorInfoDto> authors = await _db.Authors
        //        .Select(x => new AuthorInfoDto() { AuthorID = x.AuthorID, AuthorName = x.Name, TotalBook = x.Book.Count() })
        //        .ToListAsync();

        //    return authors;
        //}
    }
}
