using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.Applications.UserOperations.Commands.CreateToken;
using WebApi.Applications.UserOperations.Commands.CreateUser;
using WebApi.Applications.UserOperations.Commands.RefresToken;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;
using WebApi.TokenOperations.Models;



namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserViewModel user)
        {
            CreateUserCommand create = new CreateUserCommand(_context, _mapper);
            create.Model = user;
            create.Handle();
            return Ok("Kullanıcı Eklendi.");
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenViewModel login)
        {
            CreateTokenCommand create = new CreateTokenCommand(_context, _mapper, _configuration);
            create.Model = login;
            var token = create.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefresToken([FromQuery] string refreshtoken)
        {
            RefresTokenCommand create = new RefresTokenCommand(_context, _configuration);
            create.RefresToken = refreshtoken;
            var resultRefresToken = create.Handle();
            return resultRefresToken;
        }
    }
}
