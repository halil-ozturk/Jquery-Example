using System;
using System.ComponentModel.DataAnnotations;


namespace kitap_yazar.DOMAIN.Models
{
    public class Author
    {
        public Author(string name)
        {
            SetName(name);
        }

        [Key]
        public int AuthorID { get; set; }

        [MaxLength(200)]
        public string Name { get; internal set; }
        public virtual ICollection<Book>? Book { get; set; }


        public Author SetName(string name)
        {
            Name = name;
            return this;
        }

    }
}
