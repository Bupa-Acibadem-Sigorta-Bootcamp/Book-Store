using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommanTestFixture>
    {
        [Theory] //TODO : Tüm olası doğrulama hatalarını kontrol eder.
        [InlineData("Yahya Erdoğan", 100, 0)]
        [InlineData("Yahya Erdoğan", 0, 0)]
        [InlineData("Yahya Erdoğan", 1, 0)]
        [InlineData("Yah", 1, 100)]
        [InlineData("Yahy", 0, 1)]
        [InlineData(" ", 100, 1)]
        [InlineData(" ", 0, 100)]
        [InlineData("", 1, 100)]
        [InlineData("", 0, 0)]
        [InlineData("", 0, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string Title, int GenreId, int PageCount)
        {
            #region Arrange

            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommandModel()
            {
                Title = Title,
                GenreId = GenreId,
                PageCount = PageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            #endregion

            #region Act

            CreateBookCommandValidator createBookCommandValidator = new CreateBookCommandValidator();
            var result = createBookCommandValidator.Validate(command);

            #endregion

            #region Assert

            result.Errors.Count.Should().BeGreaterThan(0);

            #endregion
        }

        [Fact] //TODO : Sadece tarihi control eder.
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommandModel()
            {
                Title = "Title",
                GenreId = 1,
                PageCount = 1,
                PublishDate = DateTime.Now.Date
            };

            CreateBookCommandValidator createBookCommandValidator = new CreateBookCommandValidator();
            var result = createBookCommandValidator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]  //TODO : Geriye hata dönmemeli, happypath
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommandModel()
            {
                Title = "Title",
                GenreId = 1,
                PageCount = 1,
                PublishDate = DateTime.Now.Date.AddYears(-5)
            };

            CreateBookCommandValidator createBookCommandValidator = new CreateBookCommandValidator();
            var result = createBookCommandValidator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}
