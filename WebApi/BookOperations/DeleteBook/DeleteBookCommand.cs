using System;
using System.Linq;
using WebApi.DataBaseOpeOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private const string Error = "Kitap Mevcut Değil, Silinemedi!";
        private const string Value = "Silindi";
        private readonly BookDbContext _context;
        public DeleteBookCommand(BookDbContext context)
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
            //throw new InvalidOperationException(Value);
        }
    }
}
