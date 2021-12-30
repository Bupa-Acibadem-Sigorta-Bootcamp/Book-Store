using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.DataBaseOpeOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommanTestFixture commanTestFixture)
        {
            _context = commanTestFixture.context;
            _mapper = commanTestFixture.Mapper;
        }

        [Fact]
        public void WhenAllReadyExistBookTitleIsGiven_InValidaOperation_ShouldBeReturn()
        {
            #region Arrange - (Hazırlık) - Aşaması

            var book = new Book()
            {
                Title = "Yahya'nın ilk testi",
                GenreId = 1,
                PageCount = 233,
                PublishDate = new DateTime(2021, 12, 30)
            };
            _context.Add(book);
            _context.SaveChanges();


            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookCommandModel() { Title = book.Title };

            #endregion

            #region act & assert - (Çalıştırma - Doğrulama) - Aşaması

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap Zaten Mevcut!");

            #endregion
        }

        [Fact] //TODO : HappyPath
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //TODO : Arrange

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookCommandModel model = new CreateBookCommandModel()
            {
                Title = "Yahya Erdoğan",
                GenreId = 1,
                PageCount = 1,
                PublishDate = DateTime.Now.Date.AddYears(-10)
            };
            command.Model = model;

            //TODO : Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //TODO : Assert
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);

            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
        }
    }
}
