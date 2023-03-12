using kitap_yazar.BLL;
using kitap_yazar.BLL.Repositories.Abstract;
using kitap_yazar.DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kitap_yazar.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServiceController : ControllerBase
    {
        private readonly IPaginationService<Book> _paginationService;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceController(IPaginationService<Book> paginationService, IUnitOfWork unitOfWork)
        {
            _paginationService = paginationService;
            _unitOfWork = unitOfWork;
        }

        [ActionName("getallbooks")]
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var cevap = _unitOfWork.BookRepository.GetAll("Author").ToList();
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return Ok(cevap);
        }

        [ActionName("gettenbooks")]
        public async Task<IActionResult> GetByCount()
        {
            var cevap = _unitOfWork.BookRepository.GetByCount("Author",10).ToList();
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return Ok(cevap);
        }

        [ActionName("getauthorbookcount")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorBookCount()
        {
            var cevap = await _unitOfWork.AuthorRepository.TotalBookCount();
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return Ok(cevap);
        }

        [ActionName("getpagecount")]
        [HttpGet]
        public int GetPageCount(int it)
        {
            var cevap = _paginationService.PaginationCount(it);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return cevap;
        }

        [ActionName("searchbookorauthor")]
        public async Task<IActionResult> SearchBooks(string? sb = null, string? sa = null)
        {
            var cevap = await _unitOfWork.BookRepository.SearchBooks(sb, sa);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return Ok(cevap);
        }

        [ActionName("getallauthors")]
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var cevap = _unitOfWork.AuthorRepository.GetAll("Book").ToList();
            _unitOfWork.Complete();
            return Ok(cevap);

        }

        [ActionName("postauthor")]
        [HttpPost]
        [Authorize]
        public IActionResult AddAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cevap = _unitOfWork.AuthorRepository.Add(author);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();

            if (cevap == true)
                return Ok("Ekleme baþarýlý");
            return BadRequest();
        }


        [ActionName("postbook")]
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cevap = _unitOfWork.BookRepository.Add(book);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();

            if (cevap == true)
                return Ok("Ekleme baþarýlý");
            return BadRequest();
        }
    }
}