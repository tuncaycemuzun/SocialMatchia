using Ardalis.Result;
using MediatR;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Application.Features.Queries.Parameter
{
    public class GenderQuery : IRequest<Result<List<GenderResponse>>>
    {
    }

    public class GenderHandler : IRequestHandler<GenderQuery, Result<List<GenderResponse>>>
    {
        private readonly IReadRepository<Gender> _repository;

        public GenderHandler(IReadRepository<Gender> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GenderResponse>>> Handle(GenderQuery request, CancellationToken cancellationToken)
        {
            var genders = await _repository.ListAsync();
            
            var response = genders.Select(x => new GenderResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Result.Success(response);
        }
    }
}
