using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;

namespace WebApi.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genres = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if (genres is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı!");
            return _mapper.Map<GenreDetailViewModel>(genres);
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
