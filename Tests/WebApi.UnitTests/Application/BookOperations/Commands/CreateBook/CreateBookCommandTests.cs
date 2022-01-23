using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange - hazırlık
            var book = new Book { Title = "Test_WhenAlreadyExistTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1900, 01, 10), GenreId = 1, AuthorId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel
            {
                Title = book.Title
            };

            // act & assert - çalıştırma & doğrulama
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut!");

            //// assert - doğrulama
        }
        // happy path
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel
            {
                Title = "Test_WhenValidInputsAreGiven_Book_ShouldBeCreated",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-20),
                GenreId = 1,
                AuthorId = 1
            };

            command.Model = model;

            // act - çalıştırma
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert - doğrulama
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.Id.Should().BeGreaterThan(0);
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
}