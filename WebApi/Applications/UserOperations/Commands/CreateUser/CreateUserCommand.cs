using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;
using WebApi.Entities;

namespace WebApi.Applications.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserViewModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);
            if (user is not null)
                throw new InvalidOperationException("Kullanıcı Zaten Mecvut!");
            user = _mapper.Map<User>(Model);
            _context.Users.Add(user);
            _context.SaveChanges();


        }
    }
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

