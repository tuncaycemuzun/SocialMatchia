using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Features.InternalQueries.Like;
using SocialMatchia.Common;
using SocialMatchia.Common.Interfaces;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class CreateLikeCommand : IRequest<Result<bool>>
    {
        public required Guid TargetUserId { get; set; }
    }

    public class CreateLikeHandler : IRequestHandler<CreateLikeCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.LikeModel.Like> _repository;
        private readonly IMediator _mediator;
        private readonly CurrentUser _currentUser;

        public CreateLikeHandler(IRepository<Domain.Models.LikeModel.Like> repository, IMediator mediator, CurrentUser currentUser)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<bool>> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var hasExistingLike = await _mediator.Send(new LikeHasExistQuery
                {
                    TargetUserId = request.TargetUserId,
                    SourceUserId = _currentUser.Id
                }, cancellationToken);

            if (hasExistingLike) return Result.Success(true);

            var like = new Domain.Models.LikeModel.Like
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
