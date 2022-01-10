using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.DataBaseOpeOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTest(CommanTestFixture commanTestFixture)
        {
            _context = commanTestFixture.context;
            _mapper = commanTestFixture.Mapper;
        }
        [Fact]
        public void WhenTryingToAddaGenreWiththeSameName_inValidation_ShouldbeReturnanErrorMessage()
        {
            var genre = new Genre()
            {
                IsActive = true,
                Name = "Fizik"
            };
            _context.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreViewModel() { Name = genre.Name };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap Türü Zaten Mevcut!");

        }
    }
}
