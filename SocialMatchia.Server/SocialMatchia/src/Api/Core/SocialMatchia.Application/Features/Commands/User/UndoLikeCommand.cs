using SocialMatchia.Domain.Models.LikeModel;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.User
{
    public class UndoLikeCommand : IRequest<Result<bool>>
    {
        public Guid TargetUserId { get; set; }
    }

    public class UndoLikeHandler(IRepository<Like> like, CurrentUser currentUser)
        : IRequestHandler<UndoLikeCommand, Result<bool>>
    {
        private readonly IRepository<Like> _like = like ?? throw new ArgumentNullException(nameof(like));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<bool>> Handle(UndoLikeCommand request, CancellationToken cancellationToken)
        {
            if (request.TargetUserId == _currentUser.Id) Result.Success(false);

            var like = await _like.FirstOrDefaultAsync(new LikeSpec(request.TargetUserId, _currentUser.Id), cancellationToken);

            if (like is null) return Result.Success(true);

            like.SetIsDeleted(true);
            await _like.UpdateAsync(like, cancellationToken);
            await _like.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
