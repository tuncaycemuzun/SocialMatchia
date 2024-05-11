using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserSocialMediaModel.Specifications;

namespace SocialMatchia.Application.Features.InternalQueries.UserSocialMedia
{
    internal class UserSocialMediaQuery : IRequest<List<Domain.Models.UserSocialMediaModel.UserSocialMedia>>
    {
        public Guid UserId { get; set; }
    }

    internal class UserSocialMediaHandler : IRequestHandler<UserSocialMediaQuery, List<Domain.Models.UserSocialMediaModel.UserSocialMedia>>
    {
        private readonly IReadRepository<Domain.Models.UserSocialMediaModel.UserSocialMedia> _repository;

        public UserSocialMediaHandler(IReadRepository<Domain.Models.UserSocialMediaModel.UserSocialMedia> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Domain.Models.UserSocialMediaModel.UserSocialMedia>> Handle(UserSocialMediaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ListAsync(new GetUserSocialMediaSpec(request.UserId), cancellationToken);
        }
    }
}
