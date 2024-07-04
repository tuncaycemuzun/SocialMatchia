namespace SocialMatchia.Application.Features.Queries
{
    public class CityQuery : IRequest<Result<List<CityResponse>>>
    {
        public Guid CountryId { get; set; }
    }

    public class CityHandler : IRequestHandler<CityQuery, Result<List<CityResponse>>>
    {
        private readonly IReadRepository<City> _city;

        public CityHandler(IReadRepository<City> city)
        {
            _city = city ?? throw new ArgumentNullException(nameof(city));
        }

        public async Task<Result<List<CityResponse>>> Handle(CityQuery request, CancellationToken cancellationToken)
        {
            var data = await _city.ListAsync(new GetCitiesByCountryIdSpec(request.CountryId));
            
            var response = data.Select(x => new CityResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Result<List<CityResponse>>.Success(response);
        }
    }

}
