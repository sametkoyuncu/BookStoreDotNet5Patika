using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                  new Book { Title = "Lean Startup", GenreId = 1, AuthorId = 1, PageCount = 200, PublishDate = new DateTime(2001, 02, 06) },
                  new Book { Title = "Herland", GenreId = 2, AuthorId = 1, PageCount = 250, PublishDate = new DateTime(2010, 03, 12) },
                  new Book { Title = "Dune", GenreId = 2, AuthorId = 3, PageCount = 250, PublishDate = new DateTime(2011, 12, 12) }
                );
        }
    }
}