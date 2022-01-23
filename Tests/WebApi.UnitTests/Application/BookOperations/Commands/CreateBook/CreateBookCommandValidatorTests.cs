using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory] //inline data verebiliyorsun, bu test sınıfının birden fazla defa bu testler için çalışmasını sağlıyor
        [InlineData("Lord of the rings", 0, 0, 0)]
        [InlineData("Lord of the rings", 0, 1, 2)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 110, 1, 1)]
        [InlineData("    ", 0, 0, 1)]
        [InlineData("Lor", 100, 0, 1)]
        [InlineData("Lor", 0, 0, 0)]
        [InlineData("Lord", 0, 1, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            // arrange - hazırlık
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                AuthorId = authorId,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            // act - çalıştırma
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert - doğrulama
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            // arrange - hazırlık
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Geç karşim geç, bekleme yapma",
                PageCount = 100,
                GenreId = 1,
                AuthorId = 1,
                PublishDate = DateTime.Now.Date
            };

            // act - çalıştırma
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert - doğrulama
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        // happy path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange - hazırlık
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Geç karşim geç, bekleme yapma",
                PageCount = 100,
                GenreId = 1,
                AuthorId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-2)
            };

            // act - çalıştırma
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert - doğrulama
            // result.Errors.Count.Should().Equals(0); - hata verdi
            result.Errors.Count.Should().Be(0);
        }
    }
}