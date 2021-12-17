using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DataBaseOpeOperations;

namespace WebApi.BookOperations.GetBookDetailQuery
{


    public class GetBookDetailQuery
    {

        private readonly BookDbContext _context;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookDbContext context)
        {
            _context = context;
        }
        public BooksDetailViewModel Handle()
        {
            var book = _context.Books.Where<Book>(b => b.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı");
            BooksDetailViewModel booksDetailView = new BooksDetailViewModel();
            booksDetailView.Title = book.Title;
            booksDetailView.Genre = ((GenreEnum)book.GenreId).ToString();
            booksDetailView.PageCount = book.PageCount;
            booksDetailView.PaublishDate = book.PublishDate.Date.ToString("dd/MM/yy");
            return booksDetailView;
        }
    }
    public class BooksDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PaublishDate { get; set; }
    }
}