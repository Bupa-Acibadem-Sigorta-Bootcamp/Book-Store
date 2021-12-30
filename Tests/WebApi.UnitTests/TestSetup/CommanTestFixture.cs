using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DataBaseOpeOperations;

namespace WebApi.UnitTests.TestSetup
{
    public class CommanTestFixture
    {
        public BookStoreDbContext context { get; set; }
        public IMapper Mapper { get; set; }

        public CommanTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
            context = new BookStoreDbContext(options);
            context.Database.EnsureCreated();
            context.AddBooks();
            context.AddGenres();

            Mapper = new MapperConfiguration(configure =>
                { configure.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
