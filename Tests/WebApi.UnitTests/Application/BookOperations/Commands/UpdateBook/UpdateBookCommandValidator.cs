using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1, "", 1, 1)]
        [InlineData(-2, "  ", 1, 2)]
        [InlineData(0, "re", 2, 2)]
        [InlineData(0, "test", 0, 2)]
        [InlineData(1, "test", 2, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int Id, string title, int genreId, int authorId)
        {
            // arrange - hazırlık
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = Id;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId
            };

            // act - çalıştırma
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // assert - doğrulama
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        // happy path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange - hazırlık
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = 1;
            command.Model = new UpdateBookModel()
            {
                Title = "Geç karşim geç, bekleme yapma",
                GenreId = 1,
                AuthorId = 1,
            };

            // act - çalıştırma
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // assert - doğrulama
            // result.Errors.Count.Should().Equals(0); - hata verdi
            result.Errors.Count.Should().Be(0);
        }
    }
}