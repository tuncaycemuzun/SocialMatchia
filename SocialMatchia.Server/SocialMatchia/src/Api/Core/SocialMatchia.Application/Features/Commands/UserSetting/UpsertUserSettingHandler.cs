﻿using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models.UserSettingModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.UserSetting
{
    public class UpsertUserSettingCommand : IRequest<Result<bool>>
    {
        public int? BeginAge { get; set; }
        public int? EndAge { get; set; }
        public Guid? CityId { get; set; }
        public Guid? GenderId { get; set; }
    }

    public class UpsertUserSettingHandler(IRepositoryBase<Domain.Models.UserSetting> repository, CurrentUser currentUser) : IRequestHandler<UpsertUserSettingCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.UserSetting> _repository = repository;
        private readonly CurrentUser _currentUser = currentUser;

        public async Task<Result<bool>> Handle(UpsertUserSettingCommand request, CancellationToken cancellationToken)
        {
            var userSettings = await _repository.FirstOrDefaultAsync(new GetUserSettingSpec(_currentUser.Id), cancellationToken);

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
