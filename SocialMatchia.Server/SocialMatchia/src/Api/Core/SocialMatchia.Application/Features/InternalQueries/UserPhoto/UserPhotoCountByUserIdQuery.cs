using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.UserPhoto
{
    internal class UserPhotoCountByUserIdQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }

    internal class UserPhotoCountHandler : IRequestHandler<UserPhotoCountByUserIdQuery, int>
    {
        private readonly IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> _repository;

        public UserPhotoCountHandler(IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(UserPhotoCountByUserIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.CountAsync(new GetCurrentUserPhotoSpec(request.UserId));
            return response;
        }
    }
}
