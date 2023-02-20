
using System;
using Microsoft.EntityFrameworkCore;


namespace kitap_yazar.Models
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost; Database=halilDB; Username=postgres; Password=123456");


        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
