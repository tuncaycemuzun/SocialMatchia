namespace SocialMatchia.Application.Features.Queries
{
    public class CountryQuery : IRequest<Result<List<CountryResponse>>>
    {

    }

    public class CountryHandler : IRequestHandler<CountryQuery, Result<List<CountryResponse>>>
    {
        private readonly IReadRepository<Country> _country;

        public CountryHandler(IReadRepository<Country> country)
        {
            _country = country ?? throw new ArgumentNullException(nameof(country));
        }

        public async Task<Result<List<CountryResponse>>> Handle(CountryQuery request, CancellationToken cancellationToken)
        {
            var countries = await _country.ListAsync();

            var response = countries.Select(c => new CountryResponse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return Result.Success(response);
        }
    }
}
