using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DataBaseOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetAuthorsQueryViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x => x.Id);
            List<GetAuthorsQueryViewModel> result = _mapper.Map<List<GetAuthorsQueryViewModel>>(authors);
            return result;
        }
    }

    public class GetAuthorsQueryViewModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
