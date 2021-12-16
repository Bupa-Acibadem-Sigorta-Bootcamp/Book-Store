using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        // private static List<Book> BookList = new List<Book>(){

        //     new Book{
        //         Id = 1,
        //         GenreId = 1,
        //         Title = "Mahmure",
        //         PageCount = 304,
        //         PublishDate = new DateTime(2021,05,17)
        //     },
        //     new Book{
        //         Id = 2,
        //         GenreId = 2,
        //         Title = "Beyin",
        //         PageCount = 205,
        //         PublishDate = new DateTime(2021,05,17)
        //     },
        //     new Book{
        //         Id = 3,
        //         GenreId = 3,
        //         Title = "Yabani Monolyalar",
        //         PageCount = 205,
        //         PublishDate = new DateTime(2018,05,17)
        //     }
        // };

        // 

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(b => b.Id).ToList<Book>();
            return bookList;
        }


        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where<Book>(b => b.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(b => b.Title == newBook.Title);
            if (book is not null)
                return BadRequest(Error);
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok(Value);
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