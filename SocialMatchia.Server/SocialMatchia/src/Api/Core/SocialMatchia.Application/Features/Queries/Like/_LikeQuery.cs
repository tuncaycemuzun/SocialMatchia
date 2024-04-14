using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.Queries.Like
{
    public class _LikeQuery : IRequest<Domain.Models.Like?>
    {
        public Guid SourceUserId { get; set; }
        public Guid TargetUserId { get; set; }
    }

    public class _LikeHandler(IReadRepositoryBase<Domain.Models.Like> repository) : IRequestHandler<_LikeQuery, Domain.Models.Like?>
    {
        private readonly IReadRepositoryBase<Domain.Models.Like> _repository = repository;

        public async Task<Domain.Models.Like?> Handle(_LikeQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new LikeGetSpec(request.SourceUserId, request.TargetUserId), cancellationToken);

            return Result.Success(response);
        }
    }
}
