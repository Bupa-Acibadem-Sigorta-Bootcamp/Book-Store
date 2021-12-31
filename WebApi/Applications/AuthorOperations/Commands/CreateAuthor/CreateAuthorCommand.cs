using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DataBaseOperations;
using WebApi.Entities;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorCommandViewModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var authors = _context.Authors.SingleOrDefault(a => a.Name == Model.Name);
            if (authors is not null)
                throw new InvalidOperationException("Yazar Zaten Mevcut!");
            authors = _mapper.Map<Author>(Model);
            _context.Authors.Add(authors);
            _context.SaveChanges();
        }

        public class CreateAuthorCommandViewModel
        {
            public string Name { get; set; }
            public string SurName { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}
