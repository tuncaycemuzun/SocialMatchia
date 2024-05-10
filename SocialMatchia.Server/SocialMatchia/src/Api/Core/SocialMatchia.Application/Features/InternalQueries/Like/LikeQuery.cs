using Ardalis.Result;
using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.InternalQueries.Like
{
    internal class LikeQuery : IRequest<Domain.Models.LikeModel.Like?>
    {
        public Guid SourceUserId { get; set; }
        public Guid TargetUserId { get; set; }
    }

    internal class LikeHandle : IRequestHandler<LikeQuery, Domain.Models.LikeModel.Like?>
    {
        private readonly IReadRepository<Domain.Models.LikeModel.Like> _repository;

        public LikeHandle(IReadRepository<Domain.Models.LikeModel.Like> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Domain.Models.LikeModel.Like?> Handle(LikeQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new LikeGetSpec(request.SourceUserId, request.TargetUserId), cancellationToken);

            return Result.Success(response);
        }
    }
}
