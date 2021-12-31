using System;
using System.Linq;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;

namespace WebApi.Applications.BookOperations.Commands.CreateBookCommand
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        private const string Error = "Kitap Mevcut Değil, Güncellenemedi!";      
        private readonly IBookStoreDbContext _context;
        public UpdateBookCommand(IBookStoreDbContext context)
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
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
            _context.SaveChanges();            
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}