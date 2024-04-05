using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Common;
using SocialMatchia.Common.Helpers;
using System.Net.Mime;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class CreateUserPhotoHandler : IRequestHandler<CreateUserPhotoCommand, Result<bool>>
    {
        private readonly IGenericRepository<SocialMatchia.Domain.Models.UserPhoto> _repository;
        private readonly IWebHostEnvironment _environment;

        public CreateUserPhotoHandler(IGenericRepository<Domain.Models.UserPhoto> repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(CreateUserPhotoCommand request, CancellationToken cancellationToken)
        {
            if (request.Photos.Count! > 0)
            {
                return Result.Error("Photos rqeuired");
            }

            if (request.Photos.Count > 7)
            {
                return Result.Error("Maximum 7 photos can be upload");
            }

            var userPhotos = new List<Domain.Models.UserPhoto>();

            foreach (var photo in request.Photos)
            {

                if (!string.IsNullOrEmpty(photo))
                {
                    var base64Data = photo.Substring(photo.IndexOf(",") + 1);
                    base64Data = base64Data.Trim('\0');

                    var dataUri = new DataUri(base64Data);
                    var contentType = dataUri.MimeType;

                    if (!Consts.MimeTypesToExtensions.TryGetValue(contentType, out var extension))
                    {
                        continue;
                    }

                    byte[] imageBytes = Convert.FromBase64String(photo);

                    string fileName = string.Join(",", "CurrentUser - ", Guid.NewGuid().ToString(), extension);

                    File.WriteAllBytes(fileName, imageBytes);
                }
            }

            return Result.Success(true);
        }
    }
}
