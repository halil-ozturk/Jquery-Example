﻿using System;

namespace kitap_yazar.BLL.DTOs
{
    public class AuthorInfoDto
    {
        public int AuthorID { get; set; }
        public string? AuthorName { get; set; }
        public int TotalBook { get; set; }
    }
}