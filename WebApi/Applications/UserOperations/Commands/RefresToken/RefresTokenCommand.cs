using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DataBaseOpeOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Applications.UserOperations.Commands.RefresToken
{
    public class RefresTokenCommand
    {
        public string RefresToken { get; set; }
        private readonly IConfiguration _configuration;
        private readonly BookStoreDbContext _context;

        public RefresTokenCommand(BookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;

            _configuration = configuration;
        }
        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.RefresToken == RefresToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccesToken(user);

                user.RefresToken = token.RefresToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Doğrulanmış Bir Oturum Açılamadı!");
            }
        }
    }
}

