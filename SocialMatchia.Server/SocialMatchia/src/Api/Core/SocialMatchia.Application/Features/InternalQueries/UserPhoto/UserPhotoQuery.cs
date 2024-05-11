using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.UserPhoto
{
    internal class UserPhotoQuery : IRequest<Domain.Models.UserPhotoModel.UserPhoto?>
    {
        public Guid UserId { get; set; }
    }

    internal class UserPhotoHandler : IRequestHandler<UserPhotoQuery, Domain.Models.UserPhotoModel.UserPhoto?>
    {
        private readonly IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> _repository;

        public UserPhotoHandler(IReadRepository<Domain.Models.UserPhotoModel.UserPhoto> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Domain.Models.UserPhotoModel.UserPhoto?> Handle(UserPhotoQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new GetUserPhotosSpec(request.UserId));
            return response;
        }
    }
}
