using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DataBaseOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorViewModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var authors = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (authors is null)
                throw new InvalidOperationException("Yazar Bulunamadı!");

            if (_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower()
                && x.SurName.ToLower() == Model.SurName.ToLower() && x.Id != AuthorId))
                throw new InvalidOperationException("Güncellenemedi, Aynı İsim ve Soyisimli Yazar Zaten Mevcut!");

            authors.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? authors.Name : Model.Name;
            authors.SurName = string.IsNullOrEmpty(Model.SurName.Trim()) ? authors.SurName : Model.SurName;

            authors.DateOfBirth = Model.DateOfBirth;
            _context.SaveChanges();
        }

        public class UpdateAuthorViewModel
        {
            public string Name { get; set; }
            public string SurName { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}
