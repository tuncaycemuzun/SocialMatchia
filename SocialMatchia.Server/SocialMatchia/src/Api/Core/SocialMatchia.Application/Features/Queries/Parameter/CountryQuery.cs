using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Application.Features.Queries.Parameter
{
    public class CountryQuery : IRequest<Result<List<CountryResponse>>>
    {

    }

    public class CountryHandler(IReadRepository<Country> country) : IRequestHandler<CountryQuery, Result<List<CountryResponse>>>
    {
        private readonly IReadRepository<Country> _country = country ?? throw new ArgumentNullException(nameof(country));

        public async Task<Result<List<CountryResponse>>> Handle(CountryQuery request, CancellationToken cancellationToken)
        {
            var countries = await _country.ListAsync(cancellationToken);

            var response = countries.Select(c => new CountryResponse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return Result.Success(response);
        }
    }
}
