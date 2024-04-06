using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.ForAppQueries.Like
{
    public class LikeGetForAppCommand : IRequest<Domain.Models.Like?>
    {
        public Guid SourceUserId { get; set; }
        public Guid TargetUserId { get; set; }
    }

    public class LikeGetForAppHandler : IRequestHandler<LikeGetForAppCommand, Domain.Models.Like?>
    {
        private readonly IReadRepositoryBase<Domain.Models.Like> _repository;

        public LikeGetForAppHandler(IReadRepositoryBase<Domain.Models.Like> repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Models.Like?> Handle(LikeGetForAppCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new LikeGetSpec(request.SourceUserId, request.TargetUserId), cancellationToken);

            return Result.Success(response);
        }
    }
}
