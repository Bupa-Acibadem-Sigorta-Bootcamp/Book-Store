using System;
using System.Linq;
using AutoMapper;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreViewModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genres = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genres is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı!");
            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı İsimli Kitap Türü Zaten Mevcut!");
            genres.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genres.Name : Model.Name;
            genres.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }
    public class UpdateGenreViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}