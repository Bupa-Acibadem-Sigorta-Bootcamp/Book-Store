using System;
using System.Linq;
using WebApi.DataBaseOpeOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        private const string Error = "Kitap Mevcut Değil, Güncellenemedi!";
        private const string Value = "Güncellendi";
        private readonly BookDbContext _context;
        public UpdateBookCommand(BookDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == BookId);
            if (book is null)
                throw new InvalidOperationException(Error);
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _context.SaveChanges();
            throw new InvalidOperationException(Value);
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}