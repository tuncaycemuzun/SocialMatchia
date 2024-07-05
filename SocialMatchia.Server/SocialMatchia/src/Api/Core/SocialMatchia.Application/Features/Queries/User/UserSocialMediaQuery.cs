using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserModel.Specification;

namespace SocialMatchia.Application.Features.Queries.User
{
    public class UserSocialMediaQuery : IRequest<Result<List<UserSocialMediaResponse>>>
    {
    }

    public class UserSocialMediaHandler(IReadRepository<UserSocialMedia> userSocialMedia, CurrentUser currentUser) : IRequestHandler<UserSocialMediaQuery, Result<List<UserSocialMediaResponse>>>
    {
        private readonly IReadRepository<UserSocialMedia> _userSocialMedia = userSocialMedia ?? throw new ArgumentNullException(nameof(userSocialMedia));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<List<UserSocialMediaResponse>>> Handle(UserSocialMediaQuery request, CancellationToken cancellationToken)
        {
            var response = await _userSocialMedia.ListAsync(new UserSocialMediaSpec(_currentUser.Id), cancellationToken);

            var data = response.Select(x => new UserSocialMediaResponse
            {
                SocialMediaId = x.SocialMediaId,
                UserName = x.UserName
            }).ToList();

            return Result.Success(data);
        }
    }
}
