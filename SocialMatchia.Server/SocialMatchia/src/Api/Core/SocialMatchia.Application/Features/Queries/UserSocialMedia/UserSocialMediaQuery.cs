using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserSocialMediaModel.Specifications;

namespace SocialMatchia.Application.Features.Queries.UserSocialMedia
{
    public class UserSocialMediaQuery : IRequest<Result<UserSocialMediaResponse>>
    {
    }

    public class UserSocialMediaHandler : IRequestHandler<UserSocialMediaQuery, Result<UserSocialMediaResponse>>
    {
        private readonly IReadRepository<Domain.Models.UserSocialMediaModel.UserSocialMedia> _userSocialMedia;
        private readonly CurrentUser _currentUser;

        public UserSocialMediaHandler(IReadRepository<Domain.Models.UserSocialMediaModel.UserSocialMedia> userSocialMedia, CurrentUser currentUser)
        {
            _userSocialMedia = userSocialMedia ?? throw new ArgumentNullException(nameof(userSocialMedia));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<UserSocialMediaResponse>> Handle(UserSocialMediaQuery request, CancellationToken cancellationToken)
        {
            var response = await _userSocialMedia.FirstOrDefaultAsync(new GetUserSocialMediaSpec(_currentUser.Id), cancellationToken);
            if (response == null)
            {
                return Result<UserSocialMediaResponse>.NotFound();
            }

            return Result<UserSocialMediaResponse>.Success(new UserSocialMediaResponse
            {
                SocialMediaId = response.SocialMediaId,
                UserName = response.UserName
            });
        }
    }
}
