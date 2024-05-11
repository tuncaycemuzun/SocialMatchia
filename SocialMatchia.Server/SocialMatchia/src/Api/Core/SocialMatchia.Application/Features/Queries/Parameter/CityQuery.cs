using Ardalis.Result;
using MediatR;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.ParameterModel;
using SocialMatchia.Domain.Models.ParameterModel.Specification;

namespace SocialMatchia.Application.Features.Queries.Parameter
{
    public class CityQuery : IRequest<Result<List<CityResponse>>>
    {
        public Guid CountryId { get; set; }
    }

    public class CityHandler : IRequestHandler<CityQuery, Result<List<CityResponse>>>
    {
        private readonly IReadRepository<City> _repository;

        public CityHandler(IReadRepository<City> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<CityResponse>>> Handle(CityQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.ListAsync(new GetCitiesByCountryIdSpec(request.CountryId));
            var response = data.Select(x => new CityResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return Result<List<CityResponse>>.Success(response);
        }
    }

}
