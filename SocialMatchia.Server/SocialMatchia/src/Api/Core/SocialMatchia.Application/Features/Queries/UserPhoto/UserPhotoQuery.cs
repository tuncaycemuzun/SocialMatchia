using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.Queries.UserPhoto
{
    public class UserPhotoQuery : IRequest<Result<List<string>>>
    {
    }

    public class UserPhotoHandler(IReadRepositoryBase<Domain.Models.UserPhoto> repository, CurrentUser currentUser) : IRequestHandler<UserPhotoQuery, Result<List<string>>>
    {
        private readonly IReadRepositoryBase<Domain.Models.UserPhoto> _repository = repository;
        private readonly CurrentUser _currentUser = currentUser;

        public async Task<Result<List<string>>> Handle(UserPhotoQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.ListAsync(new GetCurrentUserPhotoSpec(_currentUser.Id), cancellationToken);
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
