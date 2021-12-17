using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DataBaseOpeOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private const string Error = "Hatalı";
        private const string Value = "Kaydedildi";
        private readonly BookDbContext _context;

        public BookController(BookDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where<Book>(b => b.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            
            CreateBook create = new CreateBook(_context);
            try
            {
                create.Model = newBook;
            create.Handle(); 
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            
            return Ok(Value);
            //Ok Value vermesem de yine mesaj dönüyor çünkü CreateBookda mesaj verdim. Burda da çalışır değiştirisek
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book is null)
                return BadRequest(Error);
            book.GenreId = updatedBook != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook != default ? updatedBook.Title : book.Title;
            _context.SaveChanges();
            return Ok(Value);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book is null)
                return BadRequest();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}