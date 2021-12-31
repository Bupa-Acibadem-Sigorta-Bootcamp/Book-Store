using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Applications.BookOperations.Queries.GetBookDetailQuery;
using WebApi.DataBaseOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetAuthorDetailQueryViewModel Handle()
        {
            var book = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (book is null)
                throw new InvalidOperationException("Yazar Bulunamadı!");
            GetAuthorDetailQueryViewModel authorDetailQueryViewModel = _mapper.Map<GetAuthorDetailQueryViewModel>(book);
            return authorDetailQueryViewModel;
        }
    }
    public class GetAuthorDetailQueryViewModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
