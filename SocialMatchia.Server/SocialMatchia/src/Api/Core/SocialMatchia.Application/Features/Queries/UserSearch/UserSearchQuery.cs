﻿namespace SocialMatchia.Application.Features.Queries
{
    public class UserSearchQuery : IRequest<Result<List<UserSearchModel>>>
    {
        public int Page { get; set; }
    }

    public class UserSearchHandle : IRequestHandler<UserSearchQuery, Result<List<UserSearchModel>>>
    {
        private readonly IReadRepository<UserInformation> _userInformation;
        private readonly IReadRepository<UserPhoto> _userPhoto;
        private readonly IReadRepository<UserSetting> _userSetting;
        private readonly IReadRepository<Like> _like;
        private readonly CurrentUser _currentUser;

        public UserSearchHandle(CurrentUser currentUser, IReadRepository<UserInformation> userInformation, IReadRepository<UserPhoto> userPhoto, IReadRepository<Like> like, IReadRepository<UserSetting> userSetting)
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

            var userSetting = await _userSetting.FirstOrDefaultAsync(new UserSettingSpec(_currentUser.Id), cancellationToken);

            if (userSetting is null)
            {
                throw new NotFoundException("User settings not found");
            }

            var userIds = await _userInformation.ListAsync(new UserInformationForSearchSpec(_currentUser.Id, userSetting, nonSearchableUserIdList), cancellationToken);

            var users = await _userInformation.PagedListAsync(new UserInformationByUserIdsSpec(userIds.Select(x => x.Id).ToList()), request.Page, cancellationToken);

            var photos = await _userPhoto.ListAsync(new UsersPhotoSpec(users.Select(x => x.UserId).ToList()));

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
