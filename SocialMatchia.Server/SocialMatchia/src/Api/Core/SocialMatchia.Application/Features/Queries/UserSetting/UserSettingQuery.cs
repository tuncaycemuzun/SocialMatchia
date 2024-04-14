using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Domain.Models.UserSettingModel.Specifications;

namespace SocialMatchia.Application.Features.Queries.UserSetting
{
    public class UserSettingQuery : IRequest<Result<UserSettingResponse>>
    {

    }
    public class UserSettingHandler(CurrentUser currentUser, IReadRepositoryBase<Domain.Models.UserSetting> repository) : IRequestHandler<UserSettingQuery, Result<UserSettingResponse>>
    {
        private readonly IReadRepositoryBase<Domain.Models.UserSetting> _repository = repository;
        private readonly CurrentUser _currentUser = currentUser;

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
