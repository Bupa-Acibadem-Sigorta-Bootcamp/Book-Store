using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataBaseOpeOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    //Id = 1,
                    GenreId = 1,
                    Title = "Mahmure",
                    PageCount = 304,
                    PublishDate = new DateTime(2021, 05, 17)
                },
                new Book
                {
                    //Id = 2,
                    GenreId = 2,
                    Title = "Beyin",
                    PageCount = 205,
                    PublishDate = new DateTime(2021, 05, 17)
                },
                new Book
                {
                    //Id = 3,
                    GenreId = 3,
                    Title = "Yabani Monolyalar",
                    PageCount = 205,
                    PublishDate = new DateTime(2018, 05, 17)
                }
            );

        }
    }
}
