using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-223)]
        [InlineData(-0.01)]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError(int Id)
        {
            // arrange - hazırlık
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.Id = Id;

            // act - çalıştırma
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // assert - doğrulama
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(2222)]
        [InlineData(99)]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError(int Id)
        {
            // arrange - hazırlık
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.Id = Id;

            // act - çalıştırma
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // assert - doğrulama
            // result.Errors.Count.Should().Equals(0); - hata verdi
            result.Errors.Count.Should().Be(0);
        }
    }
}