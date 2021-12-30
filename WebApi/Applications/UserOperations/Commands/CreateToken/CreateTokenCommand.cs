using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenViewModel Model { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccesToken(user);

                user.RefresToken = token.RefresToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();
                return token;

            }else
            {
                throw new InvalidOperationException("Kullanıcı Adınız veya Şifreniz Hatalı!");
            }
        }
    }
    public class CreateTokenViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

