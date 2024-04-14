using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Application.Features.Queries.Like;
using SocialMatchia.Common;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class CreateLikeCommand : IRequest<Result<bool>>
    {
        public required Guid TargetUserId { get; set; }
    }

    public class CreateLikeHandler(IRepositoryBase<Domain.Models.Like> repository, CurrentUser currentUser, IMediator mediator) : IRequestHandler<CreateLikeCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.Like> _repository = repository;
        private readonly IMediator _mediator = mediator;
        private readonly CurrentUser _currentUser = currentUser;

        public async Task<Result<bool>> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var hasExistingLike = await _mediator
                .Send(new _LikeHasExistQuery
                {
                    TargetUserId = request.TargetUserId,
                    SourceUserId = _currentUser.Id
                }, cancellationToken);

            if (hasExistingLike) return Result.Success(true);

            var like = new Domain.Models.Like
            {
                SourceUserId = _currentUser.Id,
                TargetUserId = request.TargetUserId,
            };

            await _repository.AddAsync(like, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
