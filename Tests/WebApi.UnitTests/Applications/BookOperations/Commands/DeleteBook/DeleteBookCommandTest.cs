using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.DataBaseOpeOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommandTest(CommanTestFixture commanTestFixture)
        {
            _context = commanTestFixture.context;
            _mapper = commanTestFixture.mapper;
        }
        [Fact]
        public void WhenDataisEnteredBook_DoesNotExist_CouldNotDeleteThrowError()
        {
            var book = new Book()
            {
                Id = 1,
                

            };
            _context.Remove(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap Mevcut Değil, Silinemedi!");
        }
    }
}
