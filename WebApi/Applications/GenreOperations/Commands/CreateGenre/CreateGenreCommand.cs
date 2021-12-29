using System;
using System.Linq;
using AutoMapper;
using WebApi.DataBaseOpeOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genres = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genres is not null)
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut!");
            genres = _mapper.Map<Genre>(Model);
            //genres = new Genre();
            //genres.Name = Model.Name;
            _context.Genres.Add(genres);
            _context.SaveChanges();
        }
    }
    public class CreateGenreViewModel
    {
        public string Name { get; set; }
    }
}