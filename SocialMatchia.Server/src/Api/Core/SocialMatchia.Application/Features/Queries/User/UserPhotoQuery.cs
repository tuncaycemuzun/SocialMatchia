using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserModel.Specification;

namespace SocialMatchia.Application.Features.Queries.User
{
    public class UserPhotoQuery : IRequest<Result<List<string>>>
    {
    }

    public class UserPhotoHandler(IReadRepository<UserPhoto> userPhoto, CurrentUser currentUser) : IRequestHandler<UserPhotoQuery, Result<List<string>>>
    {
        private readonly IReadRepository<UserPhoto> _userPhoto = userPhoto ?? throw new ArgumentNullException(nameof(userPhoto));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<List<string>>> Handle(UserPhotoQuery request, CancellationToken cancellationToken)
        {
            var response = await _userPhoto.ListAsync(new UserPhotosSpec(_currentUser.Id), cancellationToken);
            var photos = new List<string>();

            foreach (var photo in response)
            {
                var file = ImageHelper.ConvertImageToBase64(string.Join("/", photo.FilePath, photo.FileName));

                if (file == null) continue;

                photos.Add(file);
            }

            return Result.Success(photos);
        }
    }
}
