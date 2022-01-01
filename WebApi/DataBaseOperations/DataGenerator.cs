using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DataBaseOpeOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider servisProvider)
        {
            using (var context = new BookStoreDbContext(servisProvider
            .GetRequiredService
            <DbContextOptions
            <BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                #region Book
                context.Books.AddRange(
               new Book
               {
                   //Id = 1, TODO : Otoincramend yaptığımız için ıd dinamik geliyor.
                   GenreId = 1,
                   AuthorId = 1,
                   Title = "Mahmure",
                   PageCount = 304,
                   PublishDate = new DateTime(2021, 05, 17)
               },
               new Book
               {
                   //Id = 2,
                   GenreId = 2,
                   AuthorId = 2,
                   Title = "Beyin",
                   PageCount = 205,
                   PublishDate = new DateTime(2021, 05, 17)
               },
               new Book
               {
                   //Id = 3,
                   GenreId = 3,
                   AuthorId = 3,
                   Title = "Yabani Monolyalar",
                   PageCount = 205,
                   PublishDate = new DateTime(2018, 05, 17)
               }
           );
                #endregion
                #region Genre
                context.Genres.AddRange(

                    new Genre
                    {
                        Name = "Sözlük"
                    },
                    new Genre
                    {
                        Name = "Roman"
                    },
                    new Genre
                    {
                        Name = "Kimya"
                    }
                );
                #endregion
                #region Author
                context.Authors.AddRange(

                    new Author
                    {
                        Name = "Yahya",
                        SurName = "Erdoğan",
                        DateOfBirth = new DateTime(2021,12,31)
                    },
                    new Author
                    {
                        Name = "Cemil",
                        SurName = "Meriç",
                        DateOfBirth = new DateTime(1992, 01, 01)
                    },
                    new Author
                    {
                        Name = "Doğan",
                        SurName = "Cüceloğlu",
                        DateOfBirth = new DateTime(1993, 01, 01)
                    }
                );
                #endregion
                context.SaveChanges();
            }
        }
    }
}