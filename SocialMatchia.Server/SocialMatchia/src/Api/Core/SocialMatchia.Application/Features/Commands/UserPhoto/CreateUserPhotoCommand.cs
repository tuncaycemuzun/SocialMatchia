using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Hosting;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Helpers;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class CreateUserPhotoCommand : IRequest<Result<bool>>
    {
        public required List<string> Photos { get; set; }
    }

    public class CreateUserPhotoHandler : IRequestHandler<CreateUserPhotoCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.UserPhotoModel.UserPhoto> _userPhoto;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly CurrentUser _currentUser;

        public CreateUserPhotoHandler(IRepository<Domain.Models.UserPhotoModel.UserPhoto> userPhoto, IHostEnvironment hostEnvironment, CurrentUser currentUser)
        {
            _userPhoto = userPhoto ?? throw new ArgumentNullException(nameof(userPhoto));
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<bool>> Handle(CreateUserPhotoCommand request, CancellationToken cancellationToken)
        {
            if (request.Photos.Count! > 0)
            {
                throw new PropertyValidationException("Photos rqeuired");
            }

            if (request.Photos.Count > 7)
            {
                throw new PropertyValidationException("Maximum 7 photos can be upload");
            }

            var userPhotos = new List<Domain.Models.UserPhotoModel.UserPhoto>();
            var hostEnvironmentPath = string.Join("/", _hostEnvironment.ContentRootPath + "wwwroot", "Folders", "UserPhotos");

            var userPhotoCount = await _userPhoto.CountAsync(new GetUserPhotosSpec(_currentUser.Id));

            foreach (var photo in request.Photos)
            {
                if (!string.IsNullOrEmpty(photo))
                {
                    var imageName = ImageHelper.CreateImage(hostEnvironmentPath, photo, _currentUser.Id.ToString());

                    if (!string.IsNullOrEmpty(imageName))
                    {
                        userPhotos.Add(new Domain.Models.UserPhotoModel.UserPhoto
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
