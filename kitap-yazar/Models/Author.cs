using Microsoft.AspNetCore.Authorization;
using System;
using System.ComponentModel.DataAnnotations;


namespace kitap_yazar.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }    

        public virtual ICollection<Book>? Book { get; set; }

    }
}
