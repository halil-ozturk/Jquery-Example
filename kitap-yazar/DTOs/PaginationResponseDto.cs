using kitap_yazar.Models;

namespace kitap_yazar.DTOs
{
    public class PaginationResponseDto
    {
        public int PageCount { get; set; }

        public int BookCount { get; set; }
        public List<BookInfoDto> Book { get; set; }
    }
}
