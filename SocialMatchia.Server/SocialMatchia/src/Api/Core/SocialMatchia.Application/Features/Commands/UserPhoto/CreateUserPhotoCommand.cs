using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Hosting;
using SocialMatchia.Application.Features.InternalQueries.UserPhoto;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Helpers;
using SocialMatchia.Common.Interfaces;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class CreateUserPhotoCommand : IRequest<Result<bool>>
    {
        public required List<string> Photos { get; set; }
    }

    public class CreateUserPhotoHandler : IRequestHandler<CreateUserPhotoCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.UserPhotoModel.UserPhoto> _repository;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IMediator _mediator;
        private readonly CurrentUser _currentUser;

        public CreateUserPhotoHandler(IRepository<Domain.Models.UserPhotoModel.UserPhoto> repository, IHostEnvironment hostEnvironment, IMediator mediator, CurrentUser currentUser)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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

            var userPhotoCount = await _mediator.Send(new UserPhotoCountByUserIdQuery()
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

            await _repository.AddRangeAsync(userPhotos, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
