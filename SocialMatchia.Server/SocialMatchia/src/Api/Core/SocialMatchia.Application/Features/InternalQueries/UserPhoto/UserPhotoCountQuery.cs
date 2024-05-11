using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.UserPhoto
{
    internal class UserPhotoCountQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }

    internal class UserPhotoCountHandler : IRequestHandler<UserPhotoCountQuery, int>
    {
        private readonly IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> _repository;

        public UserPhotoCountHandler(IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(UserPhotoCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.CountAsync(new GetUserPhotosSpec(request.UserId));
            return response;
        }
    }
}
