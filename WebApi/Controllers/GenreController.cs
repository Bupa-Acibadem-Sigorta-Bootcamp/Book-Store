using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.DataBaseOpeOperations;
using WebApi.DataBaseOperations;

namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validate = new GetGenreDetailQueryValidator();
            validate.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateGenreCommand([FromBody] CreateGenreViewModel newGenre)
        {
            CreateGenreCommand create = new CreateGenreCommand(_context, _mapper);
            create.Model = newGenre;
            CreateGenreCommandValidator validate = new CreateGenreCommandValidator();
            validate.ValidateAndThrow(create);
            create.Handle();
            return Ok("Kitap Türü Eklendi.");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGenreCommand(int id, [FromBody] UpdateGenreViewModel updateGenre)
        {
            UpdateGenreCommand update = new UpdateGenreCommand(_context);
            update.GenreId = id;
            update.Model = updateGenre;
            UpdateGenreCommandValidator validate = new UpdateGenreCommandValidator();
            validate.ValidateAndThrow(update);
            update.Handle();
            return Ok("Kitap Türü Güncellendi.");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGenreCommand(int id)
        {
            DeleteGenreCommand delete = new DeleteGenreCommand(_context);
            delete.GenreId = id;
            DeleteGenreCommandValidator validate = new DeleteGenreCommandValidator();
            delete.Handle();
            return Ok("Kitap Türü Silindi.");
        }
    }
}