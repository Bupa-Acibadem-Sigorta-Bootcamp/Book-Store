using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>(){

            new Book{
                Id = 1,
                GenreId = 1,
                Title = "Mahmure",
                PageCount = 304,
                PublishDate = new DateTime(2021,05,17)
            },
            new Book{
                Id = 2,
                GenreId = 2,
                Title = "Beyin",
                PageCount = 205,
                PublishDate = new DateTime(2021,05,17)
            },
            new Book{
                Id = 3,
                GenreId = 3,
                Title = "Yabani Monolyalar",
                PageCount = 205,
                PublishDate = new DateTime(2018,05,17)
            }
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(b => b.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where<Book>(b => b.Id == id).SingleOrDefault();
            return book;
        }

        //Query kullanım tercih edilmemelidir. Sebebi ise id parametre ile karışıyor olmasıdır.

        // [HttpGet]
        // public Book Get([FromQuery]string id){
        //     var book = BookList.Where<Book>(b=> b.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(b => b.Title == newBook.Title);
            if (book is not null)
                return BadRequest();
            BookList.Add(newBook);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(b => b.Id == id);
            if (book is null)
                return BadRequest();
            book.GenreId = updatedBook != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook != default ? updatedBook.Title : book.Title;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(b=> b.Id == id);
            if(book is null)
            return BadRequest();
            BookList.Remove(book);
            return Ok();
        }
    }
}