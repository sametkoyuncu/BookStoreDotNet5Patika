using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
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
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.Id = Id;
            // act & assert - çalıştırma & doğrulama
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı!");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidIdIsGiven_Book_ShouldBeReturn(int Id)
        {
            // arrange
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.Id = Id;
            // act & assert - çalıştırma & doğrulama
            FluentActions
                .Invoking(() => command.Handle())
                .Should().NotBeNull();
        }
    }
}