using kitap_yazar.BLL.DTOs;
using kitap_yazar.BLL.Services.Kitap;
using Microsoft.AspNetCore.Mvc;

namespace kitap_yazar.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServiceController : ControllerBase
    {
        private readonly IKitapService _kitapService;
        private readonly IAuthorService _authorService;

        public ServiceController(IAuthorService authorService, IKitapService kitapService)
        {
            _authorService = authorService;
            _kitapService = kitapService;
        }

        [ActionName("getallbooks")]
        [HttpGet]
        public async Task<IActionResult> GetBooks(int bc, int pn)
        {
            var cevap = await _kitapService.GetBooks(bc, pn);
            return Ok(cevap);
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