using System;
using System.Linq;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;

namespace WebApi.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private const string Error = "Kitap Mevcut Değil, Silinemedi!";        
        private readonly IBookStoreDbContext _context;
        public DeleteBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == BookId);
            if (book is null)
                throw new InvalidOperationException(Error);
            _context.Books.Remove(book);
            _context.SaveChanges();            
        }
    }
}
