using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DataBaseOpeOperations;

namespace WebApi.BookOperations.GetBooks
{

    
    public class GetBooksQuery
    {
        private readonly BookDbContext _context;
        public GetBooksQuery(BookDbContext context)
        {
            _context = context;
        }

        public List<BooksViewModel> Handle (){
            var bookList = _context.Books.OrderBy(b => b.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PaublishDate = book.PublishDate.Date.ToString("dd/MM/yy"),
                    PageCount = book.PageCount
                });
            }
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string  Title { get; set; }  
        public int PageCount { get; set; }
        public string PaublishDate { get; set; }
        public string Genre { get; set; }
    }
}
