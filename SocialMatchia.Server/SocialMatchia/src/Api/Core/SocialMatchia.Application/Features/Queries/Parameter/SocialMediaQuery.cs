using Ardalis.Result;
using MediatR;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Application.Features.Queries.Parameter
{
    public class SocialMediaQuery : IRequest<Result<List<SocialMediaResponse>>>
    {
    }

    public class SocialMediaHandler : IRequestHandler<SocialMediaQuery, Result<List<SocialMediaResponse>>>
    {
        private readonly IReadRepository<SocialMedia> _repository;

        public SocialMediaHandler(IReadRepository<SocialMedia> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<SocialMediaResponse>>> Handle(SocialMediaQuery request, CancellationToken cancellationToken)
        {
            var socialMedias = await _repository.ListAsync();
            
            var response = socialMedias.Select(x => new SocialMediaResponse
            {
                Id = x.Id,
                Name = x.Name,
                IconName = x.IconName,
                Order = x.Order
            }).ToList();

            return Result.Success(response);
        }
    }
}
