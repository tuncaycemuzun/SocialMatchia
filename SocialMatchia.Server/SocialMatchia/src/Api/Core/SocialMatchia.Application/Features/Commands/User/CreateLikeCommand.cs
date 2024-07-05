using SocialMatchia.Domain.Models.LikeModel;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.User
{
    public class CreateLikeCommand : IRequest<Result<bool>>
    {
        public required Guid TargetUserId { get; set; }
    }

    public class CreateLikeHandler(IRepository<Like> like, CurrentUser currentUser) : IRequestHandler<CreateLikeCommand, Result<bool>>
    {
        private readonly IRepository<Like> _like = like ?? throw new ArgumentNullException(nameof(like));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<bool>> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            if (request.TargetUserId == _currentUser.Id) Result.Success(false);

            var hasExistingLike = await _like.AnyAsync(new LikeSpec(request.TargetUserId, _currentUser.Id), cancellationToken);

            if (hasExistingLike) return Result.Success(true);

            var like = new Like
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
