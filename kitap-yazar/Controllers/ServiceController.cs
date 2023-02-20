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
        [ActionName("getbook")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookInfoDto>> GetBook(int id)
        {
            BookInfoDto2 book = await _db.Books
            .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name })
            .SingleOrDefaultAsync(x => x.BookID == id);

            if (book == null)
                return NotFound("Ýstenilen kitap bulunamadý");
            return Ok(book);
        }



        [ActionName("getallbooks")]
        [HttpGet("{ks} , {sn}")]
        public async Task<IActionResult> GetBooks(int ks, int sn)
        {
            var gelecekKitap = (sn - 1) * 10;
            if (sn == 1)
            {
                List<BookInfoDto2> books = await _db.Books
                .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name }).Take(ks)
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
                List<BookInfoDto2> books = await _db.Books.Where(x=>x.Name.Length == gelecekKitap).Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name }).Take(ks).ToListAsync();
                return Ok(books);
            }
        }

        [ActionName("getbooks")]
        public async Task<IActionResult> GetBooks()
        {
            {
                List<BookInfoDto2> books = await _db.Books
                .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name }).Take(10)
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

        [ActionName("author")]
        [HttpGet("{id}")]
        public IActionResult GetBooksByAuthor(int id)
        {
            List<BookInfoDto2> books = _db.Books
                    .Where(x => x.AuthorID == id)
                    .Select(x => new BookInfoDto2() { BookID = x.BookID, Name = x.Name, AuthorName = x.Author.Name })
                    .ToList();


            if (books.Count == 0)
                return Ok("yazarýn hiç kitabý yok");
            return Ok(books);
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