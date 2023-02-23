using kitap_yazar.DTOs;
using kitap_yazar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kitap_yazar.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServiceController : ControllerBase
    {

        private readonly DatabaseContext _db;

        public ServiceController(DatabaseContext context)
        {
            _db = context;
        }


        [ActionName("getallbooks")]
        //[HttpGet("{bc}/{pn}")]

        public async Task<IActionResult> GetBooks(int bc, int pn)
        {
            var gelecekKitap = (pn - 1) * 10;
            if (gelecekKitap == 0)
            {
                List<BookInfoDto2> books = await _db.Books
            .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name }).Take(bc)
            .ToListAsync();
                if (books.Count == 0)
                {
                    return Ok("Kitap yok!");
                }
                else
                {
                    return Ok(books);
                }
            }
            else
            {
                List<BookInfoDto2> books = await _db.Books.Include(x => x.Author)
           .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name }).Skip(gelecekKitap).Take(bc)
           .ToListAsync();

                if (books.Count == 0)
                {
                    return Ok("Kitap yok!");
                }
                else
                {
                    return Ok(books);
                }
            }
        }


        [ActionName("searchbookorauthor")]
        public async Task<IActionResult> SearchBooks(string? sb = null, string? sa = null)
        {
            List<BookInfoDto2> books = await _db.Books.Include(x => x.Author)
           .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name })
           .Where(x => x.Name.Contains(sb) || x.AuthorName.Contains(sa))
           .ToListAsync();

            if (books.Count == 0)
            {
                return Ok("Kitap yok!");
            }
            else
            {
                return Ok(books);
            }
        }


        [ActionName("getallauthors")]
        [HttpGet]
        public IActionResult GetAuthors()
        {
            List<AuthorInfoDto> authors = _db.Authors
                .Select(x => new AuthorInfoDto() { AuthorID = x.AuthorID, AuthorName = x.Name, TotalBook = x.Book.Count() })
                .ToList();
            if (authors.Count == 0)
            {
                return Ok("Yazar yok!");
            }
            return Ok(authors);

        }



        [ActionName("postauthor")]
        [HttpPost]
        public IActionResult AddAuthor([FromBody] AuthorInfoDto authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var author = new Author
            {
                Name = authorDto.AuthorName
            };

            _db.Authors.Add(author);
            _db.SaveChanges();

            return Ok("Ekleme baþarýlý");
        }


        [ActionName("postbook")]
        [HttpPost]
        public IActionResult AddBook([FromBody] BookInfoDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = new Book
            {
                Name = bookDto.Name,
                AuthorID = bookDto.AuthorID,
            };

            if (book.AuthorID == 0)
                return NotFound("Yazar bulunamadý");

            _db.Books.Add(book);
            _db.SaveChanges();
            return Ok("Ekleme Baþarýlý");
        }
    }
}