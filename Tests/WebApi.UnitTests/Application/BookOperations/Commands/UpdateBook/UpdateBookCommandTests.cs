using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(999)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(10)]
        public void WhenNotExistIdIsGiven_InvalidOperationException_ShouldBeReturn(int Id)
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Id = Id;
            // act & assert - çalıştırma & doğrulama
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı!");
        }

        // happy path
        [Theory]
        [InlineData(1, "Test_WhenValidInputsAreGiven_Book_ShouldBeUpdated_1", 1, 1)]
        [InlineData(2, "Test_WhenValidInputsAreGiven_Book_ShouldBeUpdated_2", 1, 2)]
        [InlineData(3, "Test_WhenValidInputsAreGiven_Book_ShouldBeUpdated_3", 2, 2)]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated(int Id, string title, int genreId, int authorId)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Id = Id;
            UpdateBookModel model = new UpdateBookModel
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId
            };

            command.Model = model;

            // act - çalıştırma
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert - doğrulama
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }

    }

}