using System;
using System.Linq;
using AutoMapper;
using WebApi.DataBaseOpeOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBook
    {
        private const string Error = "Kitap Mevcut";
        private const string Value = "Kaydedildi";
        public CreateBookModel Model { get; set; }
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;
        public CreateBook(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(b => b.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException(Error);
            book = _mapper.Map<Book>(Model); //new Book();
            /* 
            Mapping Kullanıldı İhtiyaç Kalmadı             
             book.Title = Model.Title;
             book.PageCount = Model.PageCount;
             book.GenreId = Model.GenreId;
             book.PublishDate = Model.PaublishDate;             
             */
            _context.Books.Add(book);
            _context.SaveChanges();
            throw new InvalidOperationException(Value);
        }

    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PaublishDate { get; set; }
    }
}