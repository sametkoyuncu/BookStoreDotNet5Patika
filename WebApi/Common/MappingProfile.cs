using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOprerations.Commands.CreateGenre;
using WebApi.Application.GenreOprerations.Queries.GetGenreDetail;
using WebApi.Application.GenreOprerations.Queries.GetGenres;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /* BOOK MAPPING */

            // 1. source 2. target
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));

            // vm.Genre = ((GenreEnum)book.GenreId).ToString();
            // vm.PublishDate = book.PublishDate.Date.ToString("dd/mmm/yyyy");

            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));


            /* GENRE MAPPING */

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();

            /* AUTHOR MAPPING */
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();

            // /* USER MAPPING */
            // CreateMap<User, UsersViewModel>();
            // CreateMap<User, UserDetailViewModel>();
            CreateMap<CreateUserModel, User>();

        }
    }
}