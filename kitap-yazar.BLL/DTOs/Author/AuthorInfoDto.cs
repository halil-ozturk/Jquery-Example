using System;

namespace kitap_yazar.BLL.DTOs.Author
{
    public class AuthorInfoDto
    {
        public int AuthorID { get; set; }
        public string? AuthorName { get; set; }
        public int TotalBook { get; set; }
    }
}
