using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = new DateTime(1994, 02, 06)
                    },
                    new Author
                    {
                        FirstName = "Jane",
                        LastName = "Lorem",
                        BirthDate = new DateTime(1987, 12, 26)
                    },
                    new Author
                    {
                        FirstName = "Jess",
                        LastName = "Ipsum",
                        BirthDate = new DateTime(1976, 01, 11)
                    }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        // Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,
                        AuthorId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 02, 06)
                    },
                    new Book
                    {
                        // Id = 2,
                        Title = "Herland",
                        GenreId = 2,
                        AuthorId = 1,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 03, 12)
                    },
                    new Book
                    {
                        // Id = 3,
                        Title = "Dune",
                        GenreId = 2,
                        AuthorId = 3,
                        PageCount = 250,
                        PublishDate = new DateTime(2011, 12, 12)
                    });
                context.SaveChanges();
            }
        }
    }
}