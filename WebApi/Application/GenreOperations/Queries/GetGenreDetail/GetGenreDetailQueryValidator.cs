using FluentValidation;

namespace WebApi.Application.GenreOprerations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0);
        }
    }
}