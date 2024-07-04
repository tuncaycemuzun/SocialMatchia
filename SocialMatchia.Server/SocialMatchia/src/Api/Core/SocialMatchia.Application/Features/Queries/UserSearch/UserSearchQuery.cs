using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.LikeModel;
using SocialMatchia.Domain.Models.LikeModel.Specifications;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;
using SocialMatchia.Domain.Models.UserSettingModel.Specifications;

namespace SocialMatchia.Application.Features.Queries.UserSearch
{
    public class UserSearchQuery : IRequest<Result<List<UserSearchModel>>>
    {
        public int Page { get; set; }
    }

    public class UserSearchHandle : IRequestHandler<UserSearchQuery, Result<List<UserSearchModel>>>
    {
        private readonly IReadRepository<Domain.Models.UserInformationModel.UserInformation> _userInformation;
        private readonly IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> _userPhoto;
        private readonly IReadRepository<Domain.Models.UserSettingModel.UserSetting> _userSetting;
        private readonly IReadRepository<Like> _like;
        private readonly CurrentUser _currentUser;

        public UserSearchHandle(CurrentUser currentUser, IReadRepository<Domain.Models.UserInformationModel.UserInformation> userInformation, IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> userPhoto, IReadRepository<Like> like, IReadRepository<Domain.Models.UserSettingModel.UserSetting> userSetting)
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _userInformation = userInformation ?? throw new ArgumentNullException(nameof(userInformation));
            _userPhoto = userPhoto ?? throw new ArgumentNullException(nameof(userPhoto));
            _like = like ?? throw new ArgumentNullException(nameof(like));
            _userSetting = userSetting ?? throw new ArgumentNullException(nameof(userSetting));
        }

        public async Task<Result<List<UserSearchModel>>> Handle(UserSearchQuery request, CancellationToken cancellationToken)
        {
            var nonSearchableUsers = await _like.ListAsync(new NonSearchableUserSpec(_currentUser.Id), cancellationToken);

            var nonSearchableUserIdList = nonSearchableUsers?.Select(x => x.TargetUserId).ToList();

            var userSetting = await _userSetting.FirstOrDefaultAsync(new GetUserSettingSpec(_currentUser.Id), cancellationToken);

            if (userSetting is null)
            {
                throw new NotFoundException("User settings not found");
            }

            var userIds = await _userInformation.ListAsync(new GetUserInformationForSearchSpec(_currentUser.Id, userSetting, nonSearchableUserIdList), cancellationToken);

            var users = await _userInformation.PagedListAsync(new GetUserInformationByUserIdsSpec(userIds.Select(x => x.Id).ToList()), request.Page, cancellationToken);

            var photos = await _userPhoto.ListAsync(new GetUsersPhotosSpec(users.Select(x => x.UserId).ToList()));

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
