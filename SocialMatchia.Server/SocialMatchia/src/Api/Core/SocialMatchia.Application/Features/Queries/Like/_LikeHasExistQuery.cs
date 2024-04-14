using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.Queries.Like
{
    public class _LikeHasExistQuery : IRequest<Result<bool>>
    {
        public Guid TargetUserId { get; set; }
        public Guid SourceUserId { get; set; }
    }

    public class _LikeHasExistHandler(IReadRepositoryBase<Domain.Models.Like> repository) : IRequestHandler<_LikeHasExistQuery, Result<bool>>
    {
        private readonly IReadRepositoryBase<Domain.Models.Like> _repository = repository;

        public async Task<Result<bool>> Handle(_LikeHasExistQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.AnyAsync(new LikeGetSpec(request.TargetUserId, request.SourceUserId), cancellationToken);
            return Result.Success(result);
        }
    }
}
