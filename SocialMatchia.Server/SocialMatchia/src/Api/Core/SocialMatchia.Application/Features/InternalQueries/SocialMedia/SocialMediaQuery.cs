using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.ParameterModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.SocialMedia
{
    internal class SocialMediaQuery : IRequest<List<Domain.Models.ParameterModel.SocialMedia>>
    {

    }

    internal class SocialMediaHandler : IRequestHandler<SocialMediaQuery, List<Domain.Models.ParameterModel.SocialMedia>>
    {
        private readonly IReadRepository<Domain.Models.ParameterModel.SocialMedia> _repository;

        public SocialMediaHandler(IReadRepository<Domain.Models.ParameterModel.SocialMedia> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Domain.Models.ParameterModel.SocialMedia>> Handle(SocialMediaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ListAsync(new GetAllSocialMediaSpec(), cancellationToken);
        }
    }
}
