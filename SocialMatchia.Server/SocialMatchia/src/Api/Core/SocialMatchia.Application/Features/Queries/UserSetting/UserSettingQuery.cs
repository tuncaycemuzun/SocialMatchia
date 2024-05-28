﻿using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserSettingModel.Specifications;

namespace SocialMatchia.Application.Features.Queries.UserSetting
{
    public class UserSettingQuery : IRequest<Result<UserSettingResponse>>
    {

    }
    public class UserSettingHandler : IRequestHandler<UserSettingQuery, Result<UserSettingResponse>>
    {
        private readonly IReadRepository<Domain.Models.UserSettingModel.UserSetting> _repository;
        private readonly CurrentUser _currentUser;

        public UserSettingHandler(IReadRepository<Domain.Models.UserSettingModel.UserSetting> repository, CurrentUser currentUser)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<UserSettingResponse>> Handle(UserSettingQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new GetUserSettingSpec(_currentUser.Id), cancellationToken);

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