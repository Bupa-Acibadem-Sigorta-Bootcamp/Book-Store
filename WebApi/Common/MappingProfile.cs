using System;
using AutoMapper;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.Applications.BookOperations.Commands.CreateBookCommand;
using WebApi.Applications.BookOperations.Queries.GetBookDetailQuery;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookCommandModel, Book>(); //Add metodu için source target
            CreateMap<Book, BooksDetailViewModel>()
            .ForMember(desc => desc.Genre, opt => opt
            .MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>()
            .ForMember(desc => desc.Genre, opt => opt
            .MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

            CreateMap<Genre, GenreQueryViewModel>();
            CreateMap<Genre, CreateGenreViewModel>();            
            CreateMap<Genre, GenreDetailViewModel>();            
        }
    }
}

