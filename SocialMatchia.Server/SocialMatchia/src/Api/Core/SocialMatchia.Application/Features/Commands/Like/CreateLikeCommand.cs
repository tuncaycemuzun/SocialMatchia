using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class CreateLikeCommand : IRequest<Result<bool>>
    {
        public required Guid TargetUserId { get; set; }
    }

    public class CreateLikeHandler : IRequestHandler<CreateLikeCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.LikeModel.Like> _like;
        private readonly CurrentUser _currentUser;

        public CreateLikeHandler(IRepository<Domain.Models.LikeModel.Like> like, CurrentUser currentUser)
        {
            _like = like ?? throw new ArgumentNullException(nameof(like));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<bool>> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var hasExistingLike = await _like.AnyAsync(new LikeGetSpec(request.TargetUserId, _currentUser.Id), cancellationToken);

            if (hasExistingLike) return Result.Success(true);

            var like = new Domain.Models.LikeModel.Like
            {
                SourceUserId = _currentUser.Id,
                TargetUserId = request.TargetUserId,
            };

            await _like.AddAsync(like, cancellationToken);
            await _like.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
