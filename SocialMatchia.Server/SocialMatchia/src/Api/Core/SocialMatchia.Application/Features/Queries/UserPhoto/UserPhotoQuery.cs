using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models;
using SocialMatchia.Domain.Models.Specifications;

namespace SocialMatchia.Application.Features.Queries
{
    public class UserPhotoQuery : IRequest<Result<List<string>>>
    {
    }

    public class UserPhotoHandler : IRequestHandler<UserPhotoQuery, Result<List<string>>>
    {
        private readonly IReadRepository<UserPhoto> _userPhoto;
        private readonly CurrentUser _currentUser;

        public UserPhotoHandler(IReadRepository<UserPhoto> userPhoto, CurrentUser currentUser)
        {
            _userPhoto = userPhoto ?? throw new ArgumentNullException(nameof(userPhoto));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<List<string>>> Handle(UserPhotoQuery request, CancellationToken cancellationToken)
        {
            var response = await _userPhoto.ListAsync(new GetUserPhotosSpec(_currentUser.Id), cancellationToken);
            var photos = new List<string>();

            foreach (var photo in response)
            {
                byte[] fileBytes = File.ReadAllBytes(string.Join("/", photo.FilePath, photo.FileName));
                string base64String = Convert.ToBase64String(fileBytes);
                photos.Add(base64String);
            }

            return Result.Success(photos);
        }
    }
}
