using Microsoft.EntityFrameworkCore;

namespace WebApi.DataBaseOpeOperations
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options): base (options){

        }
        public DbSet<Book> Books {get; set;}
    }
}