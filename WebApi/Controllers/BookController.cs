using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetailQuery;
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
        public IActionResult GetById(int id)
        {
            BooksDetailViewModel result;
            try
            {
                GetBookDetailQuery getBookDetail = new GetBookDetailQuery(_context);
                getBookDetail.BookId = id;
                result = getBookDetail.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


            return Ok(result);
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
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand updateBook = new UpdateBookCommand(_context);
                updateBook.BookId = id;
                updateBook.Model = updatedBook;
                updateBook.Handle();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand deleteBook = new DeleteBookCommand(_context);
                deleteBook.BookId = id;
                deleteBook.Handle();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok("silindi");
        }
    }
}