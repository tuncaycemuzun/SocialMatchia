using SocialMatchia.Domain.Models.LikeModel;
using SocialMatchia.Domain.Models.LikeModel.Specifications;
using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserModel.Specification;

namespace SocialMatchia.Application.Features.Queries.User
{
    public class UserSearchQuery : IRequest<Result<List<UserSearchModel>>>
    {
        public int Page { get; set; }
    }

    public class UserSearchHandle(
        CurrentUser currentUser,
        IReadRepository<Domain.Models.UserModel.User> user,
        IReadRepository<UserPhoto> userPhoto,
        IReadRepository<Like> like,
        IReadRepository<UserSetting> userSetting)
        : IRequestHandler<UserSearchQuery, Result<List<UserSearchModel>>>
    {
        private readonly IReadRepository<Domain.Models.UserModel.User> _user = user ?? throw new ArgumentNullException(nameof(user));
        private readonly IReadRepository<UserPhoto> _userPhoto = userPhoto ?? throw new ArgumentNullException(nameof(userPhoto));
        private readonly IReadRepository<UserSetting> _userSetting = userSetting ?? throw new ArgumentNullException(nameof(userSetting));
        private readonly IReadRepository<Like> _like = like ?? throw new ArgumentNullException(nameof(like));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<List<UserSearchModel>>> Handle(UserSearchQuery request, CancellationToken cancellationToken)
        {
            var nonSearchableUsers = await _like.ListAsync(new NonSearchableUserSpec(_currentUser.Id), cancellationToken);

            var nonSearchableUserIdList = nonSearchableUsers.Select(x => x.TargetUserId).ToList();

            var userSetting = await _userSetting.FirstOrDefaultAsync(new UserSettingSpec(_currentUser.Id), cancellationToken);

            if (userSetting is null)
            {
                throw new NotFoundException("User settings not found");
            }

            var userIds = await _user.ListAsync(new UserInformationForSearchSpec(_currentUser.Id, userSetting, nonSearchableUserIdList), cancellationToken);

            if (!userIds.Any()) return Result.Success(new List<UserSearchModel>());

            var users = await _user.PagedListAsync(new UserInformationByUserIdsSpec(userIds.Select(x => x.Id).ToList()), request.Page, cancellationToken);

            var photos = await _userPhoto.ListAsync(new UsersPhotoSpec(users.Select(x => x.Id).ToList()));

            var result = new List<UserSearchModel>();

            foreach (var user in users)
            {
                var userPhotos = photos.Where(x => x.UserId == user.Id).ToList();
                var responsePhotos = new List<string>();

                foreach (var photo in userPhotos)
                {
                    var file = ImageHelper.ConvertImageToBase64(string.Join("/", photo.FilePath, photo.FileName));
                    if (file is null) continue;
                    responsePhotos.Add(file);
                }

                result.Add(new UserSearchModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName!,
                    LastName = user.LastName!,
                    Photos = responsePhotos,
                    City = user.City.Name
                });
            }

            return Result.Success(result);
        }
    }
}
