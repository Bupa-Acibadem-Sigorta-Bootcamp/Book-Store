using System;
using AutoMapper;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.Applications.BookOperations.Commands.CreateBookCommand;
using WebApi.Applications.BookOperations.Queries.GetBookDetailQuery;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Applications.UserOperations.Commands.CreateToken;
using WebApi.Applications.UserOperations.Commands.CreateUser;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookCommandModel, Book>(); //Add metodu için source target

            CreateMap<Book, BooksDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt
                    .MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt
                    .MapFrom(src => src.Author.Name + " " + src.Author.SurName));

            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre, opt => opt
                    .MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt
                    .MapFrom(src => src.Author.Name + " " + src.Author.SurName));

            CreateMap<CreateGenreViewModel, Genre>();
            CreateMap<Genre, GenreQueryViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();   

            CreateMap<CreateUserViewModel, User>();
            CreateMap<CreateTokenViewModel, User>();

            CreateMap<CreateAuthorCommand.CreateAuthorCommandViewModel, Author>();
            CreateMap<Author, GetAuthorDetailQueryViewModel>();
            CreateMap<Author, GetAuthorsQueryViewModel>();
        }
    }
}
