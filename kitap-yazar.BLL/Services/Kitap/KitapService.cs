using kitap_yazar.BLL.DTOs;
using kitap_yazar.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kitap_yazar.BLL.Services.Kitap
{
    public class KitapService : IKitapService
    {


        public KitapService(KitapYazarDatabaseContext context)
        {
            _db = context;
        }
        private readonly KitapYazarDatabaseContext _db;

        public async Task<List<BookInfoDto2>> GetBooks(int bc, int pn)
        {
            var gelecekKitap = (pn - 1) * 10;
            if (gelecekKitap == 0)
            {
                List<BookInfoDto2> books = await _db.Books
            .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name }).Take(bc)
            .ToListAsync();

                return books;
            }
            else
            {
                List<BookInfoDto2> books = await _db.Books.Include(x => x.Author)
           .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name }).Skip(gelecekKitap).Take(bc)
           .ToListAsync();

                return books;
            }
        }

        public async Task<List<BookInfoDto2>> SearchBooks(string? sb = null, string? sa = null)
        {
            List<BookInfoDto2> books = await _db.Books.Include(x => x.Author)
          .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name })
          .Where(x => x.Name.Contains(sb) || x.AuthorName.Contains(sa))
          .ToListAsync();

            return books;
        }

        public BookInfoDto AddBook(BookInfoDto bookInfoDto)
        {
            var book = new Book();
            {
                book.Name = bookInfoDto.Name;
                book.AuthorID = bookInfoDto.AuthorID;
            };

            _db.Books.Add(book);
            _db.SaveChanges();
            return bookInfoDto;
        }
    }
}
