using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Common;

namespace SocialMatchia.Application.Features.Commands.UserSetting
{
    public class UpsertUserSettingHandler : IRequestHandler<UpsertUserSettingCommand, Result<bool>>
    {
        private readonly IGenericRepository<Domain.Models.UserSetting> _repository;
        private readonly CurrentUser _currentUser;
        public UpsertUserSettingHandler(IGenericRepository<Domain.Models.UserSetting> repository, CurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(UpsertUserSettingCommand request, CancellationToken cancellationToken)
        {
            var userSettings = await _repository.FirstOrDefaultAsync(x => x.UserId == _currentUser.Id);

            var update = userSettings != null ? true : false;

            userSettings ??= new Domain.Models.UserSetting() { UserId = _currentUser.Id };

            userSettings.SetUserSetting(userSettings);

            if(update)
            {
                await _repository.AddAsync(userSettings);
            }
            else
            {
                await _repository.UpdateAsync(userSettings);
            }

            return Result.Success(true);
        }
    }
}
