using kitap_yazar.BLL.DTOs.Book;

namespace kitap_yazar.BLL.DTOs.Helper
{
    public class PaginationResponseDto<T> where T : class
    {
        public int PageCount { get; set; }

        public int ItemCount { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
