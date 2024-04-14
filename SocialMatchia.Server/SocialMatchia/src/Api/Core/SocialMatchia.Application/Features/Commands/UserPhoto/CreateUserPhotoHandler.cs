using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.Hosting;
using SocialMatchia.Application.Features.ForAppQueries.UserPhoto;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Helpers;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class CreateUserPhotoCommand : IRequest<Result<bool>>
    {
        public required List<string> Photos { get; set; }
    }

    public class CreateUserPhotoHandler(IRepositoryBase<Domain.Models.UserPhoto> repository, IHostEnvironment hostEnvironment, CurrentUser currentUser, IMediator mediator) : IRequestHandler<CreateUserPhotoCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.UserPhoto> _repository = repository;
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IMediator _mediator = mediator;
        private readonly CurrentUser _currentUser = currentUser;

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

            var userPhotoCount = await _mediator.Send(new UserPhotoGetCommand()
            {
                UserId = _currentUser.Id
            }, cancellationToken);

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

            await _repository.AddRangeAsync(userPhotos, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
