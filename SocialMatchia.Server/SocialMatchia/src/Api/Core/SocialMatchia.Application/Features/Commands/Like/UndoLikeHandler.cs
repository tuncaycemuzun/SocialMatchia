using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class UndoLikeHandler : IRequestHandler<UndoLikeCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.Like> _repository;
        private readonly CurrentUser _currentUser;

        public UndoLikeHandler(IRepositoryBase<Domain.Models.Like> repository, CurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(UndoLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _repository.FirstOrDefaultAsync(new LikeCheckHasExistSpec(request.TargetUserId, _currentUser.Id));

            if (like is null) return Result.Success(true);

            like.SetIsDeleted(true);
            await _repository.UpdateAsync(like, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
