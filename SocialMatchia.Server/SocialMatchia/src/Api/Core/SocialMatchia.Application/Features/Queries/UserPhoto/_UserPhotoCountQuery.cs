using Ardalis.Specification;
using MediatR;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.Queries.UserPhoto
{
    public class _UserPhotoCountQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }

    public class _UserPhotoCountHandler(IRepositoryBase<Domain.Models.UserPhoto> repository) : IRequestHandler<_UserPhotoCountQuery, int>
    {
        private readonly IRepositoryBase<Domain.Models.UserPhoto> _repository = repository;

        public async Task<int> Handle(_UserPhotoCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.CountAsync(new GetCurrentUserPhotoSpec(request.UserId));
            return response;
        }
    }
}
