using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.InternalQueries.Like
{
    internal class LikeHasExistQuery : IRequest<Result<bool>>
    {
        public Guid TargetUserId { get; set; }
        public Guid SourceUserId { get; set; }
    }

    internal class LikeHasExistHandler : IRequestHandler<LikeHasExistQuery, Result<bool>>
    {
        private readonly IReadRepository<Domain.Models.LikeModel.Like> _repository;

        public LikeHasExistHandler(IReadRepository<Domain.Models.LikeModel.Like> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Result<bool>> Handle(LikeHasExistQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.AnyAsync(new LikeGetSpec(request.TargetUserId, request.SourceUserId), cancellationToken);
            return Result.Success(result);
        }
    }
}
