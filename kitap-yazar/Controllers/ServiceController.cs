using kitap_yazar.BLL.DTOs.Author;
using kitap_yazar.BLL.DTOs.Book;
using kitap_yazar.BLL.Services.Helpers;
using kitap_yazar.BLL.Services.Kitap;
using kitap_yazar.BLL.Services.Yazar;
using kitap_yazar.DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kitap_yazar.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServiceController : ControllerBase
    {
        private readonly IKitapService _kitapService;
        private readonly IAuthorService _authorService;
        private readonly IPaginationService<Book> _paginationService;
        public ServiceController(IAuthorService authorService, IKitapService kitapService, IPaginationService<Book> paginationService)
        {
            _authorService = authorService;
            _kitapService = kitapService;
            _paginationService = paginationService;
        }

        [ActionName("getallbooks")]
        [HttpGet]
        public async Task<IActionResult> GetBooks(int bc, int pn)
        {
            var cevap = await _paginationService.GetPaginationResponses(bc, pn, include: x=>x.Include(y=>y.Author));
            return Ok(cevap);
        }

        [ActionName("getpagecount")]
        [HttpGet]
        public int GetPageCount(int it)
        {
            var cevap = _paginationService.PaginationCount(it);
            return cevap;
        }

        [ActionName("searchbookorauthor")]
        public async Task<IActionResult> SearchBooks(string? sb = null, string? sa = null)
        {
            var cevap = await _kitapService.SearchBooks(sb, sa);
            return Ok(cevap);
        }

        [ActionName("getallauthors")]
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var cevap = await _authorService.GetAuthors();
            return Ok(cevap);
        }

        [ActionName("postauthor")]
        [HttpPost]
        public IActionResult AddAuthor([FromBody] AuthorInfoDto authorInfoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cevap = _authorService.AddAuthor(authorInfoDto);

            if (cevap != null)
                return Ok("Ekleme baþarýlý");
            return BadRequest();
        }


        [ActionName("postbook")]
        [HttpPost]
        public IActionResult AddBook([FromBody] BookInfoDto bookInfoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cevap = _kitapService.AddBook(bookInfoDto);
            if (cevap != null)
                return Ok("Ekleme baþarýlý");
            return BadRequest();
        }
    }
}