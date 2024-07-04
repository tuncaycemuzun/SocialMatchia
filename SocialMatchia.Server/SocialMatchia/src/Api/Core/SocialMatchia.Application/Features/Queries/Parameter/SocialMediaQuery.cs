using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Application.Features.Queries.Parameter
{
    public class SocialMediaQuery : IRequest<Result<List<SocialMediaResponse>>>
    {
    }

    public class SocialMediaHandler : IRequestHandler<SocialMediaQuery, Result<List<SocialMediaResponse>>>
    {
        private readonly IReadRepository<SocialMedia> _socialMedia;

        public SocialMediaHandler(IReadRepository<SocialMedia> socialMedia)
        {
            _socialMedia = socialMedia ?? throw new ArgumentNullException(nameof(socialMedia));
        }

        public async Task<Result<List<SocialMediaResponse>>> Handle(SocialMediaQuery request, CancellationToken cancellationToken)
        {
            var socialMedias = await _socialMedia.ListAsync();

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
