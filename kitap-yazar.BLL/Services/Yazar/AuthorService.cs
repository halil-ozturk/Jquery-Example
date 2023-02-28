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

            



            //select a."AuthorID" from public."Authors" a full Outer join public."Books" b on a."AuthorID" = b."AuthorID"
	        //where b."BookID" is null

            
            return authors;
        }

        public AuthorInfoDto AddAuthor(AuthorInfoDto authorInfoDto)
        {
            var author = new Author(authorInfoDto.AuthorName);

            _db.Authors.Add(author);
            _db.SaveChanges();

            return authorInfoDto;
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
    }
}
