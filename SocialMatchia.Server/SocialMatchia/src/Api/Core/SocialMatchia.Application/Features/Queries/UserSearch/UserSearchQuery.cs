using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Features.InternalQueries.UserInformation;
using SocialMatchia.Application.Features.InternalQueries.UserSearch;
using SocialMatchia.Application.Features.InternalQueries.UserSetting;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserModel.Specifications;

namespace SocialMatchia.Application.Features.Queries.UserSearch
{
    public class UserSearchQuery : IRequest<Result<PaginationModel<List<UserSearchModel>>>>
    {
        public int Page { get; set; } = 1;
    }

    public class UserSearchHandle : IRequestHandler<UserSearchQuery, Result<PaginationModel<List<UserSearchModel>>>>
    {
        private readonly IReadRepository<User> _repository;
        private readonly CurrentUser _currentUser;
        private readonly IMediator _mediator;

        public UserSearchHandle(IReadRepository<User> repository, CurrentUser currentUser, IMediator mediator)
        {
            _repository = repository;
            _currentUser = currentUser;
            _mediator = mediator;
        }

        public async Task<Result<PaginationModel<List<UserSearchModel>>>> Handle(UserSearchQuery request, CancellationToken cancellationToken)
        {
            var nonSearchableUserIdList = await _mediator.Send(new NonSearchableUserQuery { UserId = _currentUser.Id });

            var userSetting = await _mediator.Send(new UserSettingQuery());

            if (userSetting is null)
            {
                throw new NotFoundException("User settings not found");
            }

            var userIds = await _mediator.Send(new UserIdsByUserInformationSearchQuery
            {
                CurrentUserId = _currentUser.Id,
                UserSetting = userSetting,
                NonSearchableUserIdList = nonSearchableUserIdList
            });

            var users = await _repository.ListAsync(new GetUsersSearchSpec(userIds), cancellationToken);

            return Result.Success();
        }
    }
}
