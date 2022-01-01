using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;
using WebApi.Entities;

namespace WebApi.Applications.BookOperations.Queries.GetBookDetailQuery
{


    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public BooksDetailViewModel Handle()
        {
            var book = _context.Books.Include(x => x.Genre).SingleOrDefault(b => b.Id == BookId);
            book = _context.Books.Include(x => x.Author).SingleOrDefault(b => b.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı!");
            BooksDetailViewModel booksDetailView = _mapper.Map<BooksDetailViewModel>(book);
            return booksDetailView;
        }
    }
    public class BooksDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}