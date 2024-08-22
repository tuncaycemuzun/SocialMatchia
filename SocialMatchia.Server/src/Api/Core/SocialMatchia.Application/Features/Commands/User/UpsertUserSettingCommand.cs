using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserModel.Specification;

namespace SocialMatchia.Application.Features.Commands.User
{
    public class UpsertUserSettingCommand : IRequest<Result<bool>>
    {
        public int BeginAge { get; set; }
        public int EndAge { get; set; }
        public Guid CityId { get; set; }
        public Guid GenderId { get; set; }
    }

    public class UpsertUserSettingHandler(IRepository<UserSetting> userSetting, CurrentUser currentUser) : IRequestHandler<UpsertUserSettingCommand, Result<bool>>
    {
        private readonly IRepository<UserSetting> _userSetting = userSetting ?? throw new ArgumentNullException(nameof(userSetting));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<bool>> Handle(UpsertUserSettingCommand request, CancellationToken cancellationToken)
        {
            var userSetting = await _userSetting.FirstOrDefaultAsync(new UserSettingSpec(_currentUser.Id), cancellationToken);

            var data = new UserSetting
            {
                CityId = request.CityId,
                GenderId = request.GenderId,
                BeginAge = request.BeginAge,
                EndAge = request.EndAge,
                UserId = _currentUser.Id
            };

            if (userSetting != null)
            {
                data.Id = userSetting.Id;
                userSetting.SetUserSetting(data);
                await _userSetting.UpdateAsync(userSetting, cancellationToken);
            }
            else
            {
                await _userSetting.AddAsync(data, cancellationToken);
            }

            await _userSetting.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
