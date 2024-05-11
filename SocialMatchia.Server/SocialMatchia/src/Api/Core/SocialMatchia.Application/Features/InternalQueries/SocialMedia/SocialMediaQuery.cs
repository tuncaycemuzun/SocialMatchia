using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.SocialMediaModel.Specifications;

namespace SocialMatchia.Application.Features.InternalQueries.SocialMedia
{
    internal class SocialMediaQuery : IRequest<List<Domain.Models.SocialMediaModel.SocialMedia>>
    {

    }

    internal class SocialMediaHandler : IRequestHandler<SocialMediaQuery, List<Domain.Models.SocialMediaModel.SocialMedia>>
    {
        private readonly IReadRepository<Domain.Models.SocialMediaModel.SocialMedia> _repository;

        public SocialMediaHandler(IReadRepository<Domain.Models.SocialMediaModel.SocialMedia> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Domain.Models.SocialMediaModel.SocialMedia>> Handle(SocialMediaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ListAsync(new GetAllSocialMediaSpec(), cancellationToken);
        }
    }
}
