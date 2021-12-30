using System;
using System.Linq;
using AutoMapper;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;
using WebApi.Entities;

namespace WebApi.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        private const string Error = "Kitap Zaten Mevcut!";        
        public CreateBookCommandModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext context, IMapper mapper)
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
        }
    }
    public class CreateBookCommandModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}