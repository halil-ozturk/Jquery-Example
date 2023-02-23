
using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;


namespace kitap_yazar.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost; Database=halilDB; Username=postgres; Password=123456");
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
