using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Features.InternalQueries.UserInformation;
using SocialMatchia.Application.Features.InternalQueries.UserPhoto;
using SocialMatchia.Application.Features.InternalQueries.UserSearch;
using SocialMatchia.Application.Features.InternalQueries.UserSetting;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserModel;

namespace SocialMatchia.Application.Features.Queries.UserSearch
{
    public class UserSearchQuery : IRequest<Result<List<UserSearchModel>>>
    {
        public int Page { get; set; }
    }

    public class UserSearchHandle : IRequestHandler<UserSearchQuery, Result<List<UserSearchModel>>>
    {
        private readonly IReadRepository<User> _repository;
        private readonly IReadRepository<Domain.Models.UserInformationModel.UserInformation> _userInformation;
        private readonly CurrentUser _currentUser;
        private readonly IMediator _mediator;

        public UserSearchHandle(IReadRepository<User> repository, CurrentUser currentUser, IMediator mediator, IReadRepository<Domain.Models.UserInformationModel.UserInformation> userInformation)
        {
            _repository = repository;
            _currentUser = currentUser;
            _mediator = mediator;
            _userInformation = userInformation;
        }

        public async Task<Result<List<UserSearchModel>>> Handle(UserSearchQuery request, CancellationToken cancellationToken)
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

            var users = await _mediator.Send(new UserInformationsQuery { UserIds = userIds, PageNumber = request.Page }, cancellationToken);

            var photos = await _mediator.Send(new UserPhotosQuery() { UserIds = users.Select(x => x.UserId).ToList() }, cancellationToken);

            var result = new List<UserSearchModel>();

            foreach (var user in users)
            {
                var userPhotos = photos?.Where(x => x.UserId == user.UserId).ToList();

                result.Add(new UserSearchModel
                {
                    Id = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Photos = userPhotos?.Select(x => x.FilePath).ToList(),
                    City = user.City.Name
                });
            }

            return Result.Success(result);
        }
    }
}
