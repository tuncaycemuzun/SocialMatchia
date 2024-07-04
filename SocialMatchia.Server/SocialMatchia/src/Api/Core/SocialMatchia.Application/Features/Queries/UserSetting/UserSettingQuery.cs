namespace SocialMatchia.Application.Features.Queries
{
    public class UserSettingQuery : IRequest<Result<UserSettingResponse>>
    {

    }
    public class UserSettingHandler : IRequestHandler<UserSettingQuery, Result<UserSettingResponse>>
    {
        private readonly IReadRepository<UserSetting> _userSetting;
        private readonly CurrentUser _currentUser;

        public UserSettingHandler(IReadRepository<UserSetting> userSetting, CurrentUser currentUser)
        {
            _userSetting = userSetting ?? throw new ArgumentNullException(nameof(userSetting));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<UserSettingResponse>> Handle(UserSettingQuery request, CancellationToken cancellationToken)
        {
            var response = await _userSetting.FirstOrDefaultAsync(new GetUserSettingSpec(_currentUser.Id), cancellationToken);

            if (response == null)
            {
                return Result<UserSettingResponse>.NotFound();
            }

            return Result<UserSettingResponse>.Success(new UserSettingResponse
            {
                BeginAge = response.BeginAge,
                EndAge = response.EndAge,
                CityId = response.CityId,
                GenderId = response.GenderId
            });
        }
    }
}
