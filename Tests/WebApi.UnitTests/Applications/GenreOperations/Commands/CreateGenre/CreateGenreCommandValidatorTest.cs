using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommanTestFixture>
    {

        [Theory] //TODO : Tüm olası doğrulama hatalarını kontrol eder.
        [InlineData("Yah")]
        [InlineData("Ya")]
        [InlineData("Y")]
        [InlineData(" ")]
        [InlineData("")]
       
        public void WhenAddingGenre_IfValidationRulesAreNotFollowed_ShouldBeReturnedAnErrors(string Name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreViewModel()
            {
                Name = Name
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenAddingGenre_WhenValidationRulesAreFollowed_NoErrorsShouldBeReturned()
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreViewModel()
            {
                Name = "Name"
            };
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Equals(0);
        }
    }
}
