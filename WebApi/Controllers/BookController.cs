using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.Applications.BookOperations.Commands.CreateBookCommand;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.Applications.BookOperations.Queries.GetBookDetailQuery;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.DataBaseOpeOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BooksDetailViewModel result;
            GetBookDetailQuery getBookDetail = new GetBookDetailQuery(_context, _mapper);
            getBookDetail.BookId = id;
            result = getBookDetail.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateBookCommand([FromBody] CreateBookCommandModel newBook)
        {
            CreateBookCommand create = new CreateBookCommand(_context, _mapper);
            create.Model = newBook;
            CreateBookCommandValidator valid = new CreateBookCommandValidator();
            valid.ValidateAndThrow(create);
            create.Handle();
            return Ok("Kitap Eklendi.");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand updateBook = new UpdateBookCommand(_context);
            updateBook.BookId = id;
            updateBook.Model = updatedBook;
            updateBook.Handle();
            _context.SaveChanges();
            return Ok("Kitap Güncellendi.");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand deleteBook = new DeleteBookCommand(_context);
            deleteBook.BookId = id;
            deleteBook.Handle();
            return Ok("Kitap Silindi.");
        }
    }
}
