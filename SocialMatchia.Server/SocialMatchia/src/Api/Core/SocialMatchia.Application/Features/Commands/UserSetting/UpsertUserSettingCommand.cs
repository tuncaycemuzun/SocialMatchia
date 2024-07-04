namespace SocialMatchia.Application.Features.Commands
{
    public class UpsertUserSettingCommand : IRequest<Result<bool>>
    {
        public int? BeginAge { get; set; }
        public int? EndAge { get; set; }
        public Guid? CityId { get; set; }
        public Guid? GenderId { get; set; }
    }

    public class UpsertUserSettingHandler : IRequestHandler<UpsertUserSettingCommand, Result<bool>>
    {
        private readonly IRepository<UserSetting> _userSetting;
        private readonly CurrentUser _currentUser;

        public UpsertUserSettingHandler(IRepository<UserSetting> userSetting, CurrentUser currentUser)
        {
            _userSetting = userSetting ?? throw new ArgumentNullException(nameof(_userSetting));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<bool>> Handle(UpsertUserSettingCommand request, CancellationToken cancellationToken)
        {
            var userSettings = await _userSetting.FirstOrDefaultAsync(new GetUserSettingSpec(_currentUser.Id), cancellationToken);

            var update = userSettings != null ? true : false;

            userSettings ??= new UserSetting { UserId = _currentUser.Id };

            userSettings.SetUserSetting(userSettings);

            if (update)
            {
                await _userSetting.AddAsync(userSettings, cancellationToken);
            }
            else
            {
                await _userSetting.UpdateAsync(userSettings, cancellationToken);
            }

            await _userSetting.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
