using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Hosting;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Helpers;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class CreateUserPhotoHandler : IRequestHandler<CreateUserPhotoCommand, Result<bool>>
    {
        private readonly IGenericRepository<Domain.Models.UserPhoto> _repository;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly CurrentUser _currentUser;

        public CreateUserPhotoHandler(IGenericRepository<Domain.Models.UserPhoto> repository, IHostEnvironment hostEnvironment, CurrentUser currentUser)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
            _currentUser = currentUser;
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

            var userPhotos = new List<Domain.Models.UserPhoto>();
            var hostEnvironmentPath = string.Join("/", _hostEnvironment.ContentRootPath + "wwwroot", "Folders", "UserPhotos");

            var index = 0;

            foreach (var photo in request.Photos)
            {
                if (!string.IsNullOrEmpty(photo))
                {
                    var imageName = ImageHelper.CreateImage(hostEnvironmentPath, photo, _currentUser.Id.ToString());

                    if (!string.IsNullOrEmpty(imageName))
                    {
                        userPhotos.Add(new Domain.Models.UserPhoto
                        {
                            FileName = imageName,
                            FilePath = Path.Combine(hostEnvironmentPath, imageName),
                            UserId = _currentUser.Id,
                            Order = index,
                        });

                        index++;
                    }
                }
            }

            if (userPhotos.Count == 0)
            {
                return Result.Success(false);
            }

            await _repository.AddRangeAsync(userPhotos);
            return Result.Success(true);
        }
    }
}
