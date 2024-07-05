using SocialMatchia.Domain.Models.ParameterModel;
using SocialMatchia.Domain.Models.ParameterModel.Specification;

namespace SocialMatchia.Application.Features.Queries.Parameter
{
    public class CityQuery : IRequest<Result<List<CityResponse>>>
    {
        public Guid CountryId { get; set; } = new Guid("cda3bbe9-a096-4120-b21d-5d34205b8da4");
    }

    public class CityHandler(IReadRepository<City> city) : IRequestHandler<CityQuery, Result<List<CityResponse>>>
    {
        private readonly IReadRepository<City> _city = city ?? throw new ArgumentNullException(nameof(city));

        public async Task<Result<List<CityResponse>>> Handle(CityQuery request, CancellationToken cancellationToken)
        {
            var data = await _city.ListAsync(new CitiesByCountryIdSpec(request.CountryId), cancellationToken);

            var response = data.Select(x => new CityResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Result<List<CityResponse>>.Success(response);
        }
    }
}
