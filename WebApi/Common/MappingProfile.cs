using System;
using AutoMapper;
using WebApi.BookOperations.AddBook;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetailQuery;
using WebApi.BookOperations.GetBooks;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>(); //Add metodu için source target
            CreateMap<Book, BooksDetailViewModel>()
            .ForMember(desc => desc.Genre, opt => opt
            .MapFrom(src => ((GenreEnum)src.GenreId).ToString()));  
            CreateMap<Book, BooksViewModel>()
            .ForMember(desc => desc.Genre, opt => opt
            .MapFrom(src => ((GenreEnum)src.GenreId).ToString()));           
        }
    }
}

