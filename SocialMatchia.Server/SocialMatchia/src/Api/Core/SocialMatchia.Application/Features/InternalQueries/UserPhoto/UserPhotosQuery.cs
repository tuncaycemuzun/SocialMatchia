using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.UserPhoto
{
    internal class UserPhotosQuery : IRequest<List<Domain.Models.UserPhotoModel.UserPhoto>?>
    {
        public required List<Guid> UserIds { get; set; }
    }

    internal class UserPhotosHandler : IRequestHandler<UserPhotosQuery, List<Domain.Models.UserPhotoModel.UserPhoto>?>
    {
        private readonly IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> _repository;

        public UserPhotosHandler(IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Domain.Models.UserPhotoModel.UserPhoto>?> Handle(UserPhotosQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.ListAsync(new GetUsersPhotosSpec(request.UserIds));
            return response;
        }
    }
}
