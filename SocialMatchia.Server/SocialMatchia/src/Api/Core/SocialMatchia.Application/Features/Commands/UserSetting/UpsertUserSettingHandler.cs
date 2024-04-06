using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models.UserSettingModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.UserSetting
{
    public class UpsertUserSettingHandler : IRequestHandler<UpsertUserSettingCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.UserSetting> _repository;
        private readonly CurrentUser _currentUser;
        public UpsertUserSettingHandler(IRepositoryBase<Domain.Models.UserSetting> repository, CurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(UpsertUserSettingCommand request, CancellationToken cancellationToken)
        {
            var userSettings = await _repository.FirstOrDefaultAsync(new GetUserSetting(_currentUser.Id), cancellationToken);

            var update = userSettings != null ? true : false;

            userSettings ??= new Domain.Models.UserSetting() { UserId = _currentUser.Id };

            userSettings.SetUserSetting(userSettings);

            if (update)
            {
                await _repository.AddAsync(userSettings, cancellationToken);
            }
            else
            {
                await _repository.UpdateAsync(userSettings, cancellationToken);
            }

            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
