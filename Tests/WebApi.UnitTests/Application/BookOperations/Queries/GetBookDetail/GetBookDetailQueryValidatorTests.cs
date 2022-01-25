using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-223)]
        [InlineData(-0.01)]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError(int Id)
        {
            // arrange - hazırlık
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.Id = Id;

            // act - çalıştırma
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
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
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.Id = Id;

            // act - çalıştırma
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            // assert - doğrulama
            // result.Errors.Count.Should().Equals(0); - hata verdi
            result.Errors.Count.Should().Be(0);
        }
    }
}