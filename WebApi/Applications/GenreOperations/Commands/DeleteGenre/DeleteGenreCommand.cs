using System;
using System.Linq;
using AutoMapper;
using WebApi.DataBaseOpeOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;           
        }
        public void Handle()
        {
            var genres = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genres is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı!");
            _context.Genres.Remove(genres);           
            _context.SaveChanges();
            
        }
    }
}