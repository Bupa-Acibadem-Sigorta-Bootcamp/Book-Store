using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DataBaseOpeOperations;
using WebApi.Entities;

namespace WebApi.Applications.BookOperations.Queries.GetBookDetailQuery
{


    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public BooksDetailViewModel Handle()
        {
            var book = _context.Books.Where<Book>(b => b.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı!");
            BooksDetailViewModel booksDetailView = _mapper.Map<BooksDetailViewModel>(book);
            return booksDetailView;
            throw new InvalidOperationException("Kitap Listelendi.");
        }
    }
    public class BooksDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}