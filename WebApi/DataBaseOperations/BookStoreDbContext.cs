using Microsoft.EntityFrameworkCore;
using WebApi.DataBaseOperations;
using WebApi.Entities;

namespace WebApi.DataBaseOpeOperations
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public DbSet<User> Users { get; set; }
    }
}