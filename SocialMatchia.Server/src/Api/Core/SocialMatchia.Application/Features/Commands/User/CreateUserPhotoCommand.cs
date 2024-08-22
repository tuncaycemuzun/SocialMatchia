using Microsoft.Extensions.Hosting;
using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserModel.Specification;


namespace SocialMatchia.Application.Features.Commands.User
{
    public class CreateUserPhotoCommand : IRequest<Result<bool>>
    {
        public required List<string> Photos { get; set; }
    }

    public class CreateUserPhotoHandler(
        IRepository<UserPhoto> userPhoto,
        IHostEnvironment hostEnvironment,
        CurrentUser currentUser)
        : IRequestHandler<CreateUserPhotoCommand, Result<bool>>
    {
        private readonly IRepository<UserPhoto> _userPhoto = userPhoto ?? throw new ArgumentNullException(nameof(userPhoto));
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<bool>> Handle(CreateUserPhotoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Photos.Any())
            {
                throw new PropertyValidationException("Photos rqeuired");
            }

            if (request.Photos.Count > 7)
            {
                throw new PropertyValidationException("Maximum 7 photos can be upload");
            }

            var userPhotos = new List<UserPhoto>();
            var hostEnvironmentPath = string.Join("/", _hostEnvironment.ContentRootPath + "/wwwroot", "Folders", "UserPhotos");

            var userPhotoCount = await _userPhoto.CountAsync(new UserPhotosSpec(_currentUser.Id), cancellationToken);

            foreach (var photo in request.Photos)
            {
                if (!string.IsNullOrEmpty(photo))
                {
                    var imageName = ImageHelper.CreateImage(hostEnvironmentPath, photo);

                    if (!string.IsNullOrEmpty(imageName))
                    {
                        userPhotos.Add(new UserPhoto
                        {
                            FileName = imageName,
                            FilePath = hostEnvironmentPath,
                            UserId = _currentUser.Id,
                            Order = userPhotoCount,
                        });

                        userPhotoCount++;
                    }
                }
            }

            if (userPhotos.Count == 0)
            {
                return Result.Success(false);
            }

            await _userPhoto.AddRangeAsync(userPhotos, cancellationToken);
            await _userPhoto.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
