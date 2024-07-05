using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserModel.Specification;

namespace SocialMatchia.Application.Features.Queries.User
{
    public class UserSettingQuery : IRequest<Result<UserSettingResponse>>
    {

    }
    public class UserSettingHandler(IReadRepository<UserSetting> userSetting, CurrentUser currentUser) : IRequestHandler<UserSettingQuery, Result<UserSettingResponse>>
    {
        private readonly IReadRepository<UserSetting> _userSetting = userSetting ?? throw new ArgumentNullException(nameof(userSetting));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<UserSettingResponse>> Handle(UserSettingQuery request, CancellationToken cancellationToken)
        {
            var response = await _userSetting.FirstOrDefaultAsync(new UserSettingSpec(_currentUser.Id), cancellationToken);

            return Result<UserSettingResponse>.Success(new UserSettingResponse
            {
                BeginAge = response?.BeginAge,
                EndAge = response?.EndAge,
                CityId = response?.CityId,
                GenderId = response?.GenderId
            });
        }
    }
}
