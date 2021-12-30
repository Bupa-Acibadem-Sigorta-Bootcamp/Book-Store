using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataBaseOpeOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{

    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
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
        }
    }
}