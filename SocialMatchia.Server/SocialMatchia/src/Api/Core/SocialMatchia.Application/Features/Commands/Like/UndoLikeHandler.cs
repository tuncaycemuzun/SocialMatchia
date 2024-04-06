using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Common;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class UndoLikeHandler : IRequestHandler<UndoLikeCommand, Result<bool>>
    {
        private readonly IGenericRepository<Domain.Models.Like> _repository;
        private readonly CurrentUser _currentUser;

        public UndoLikeHandler(IGenericRepository<Domain.Models.Like> repository, CurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(UndoLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _repository.FirstOrDefaultAsync(x => x.TargetUserId == request.TargetUserId && !x.IsDeleted && x.SourceUserId == _currentUser.Id);

            if(like is null) return Result.Success(true);

            like.SetIsDeleted(true);
            await _repository.UpdateAsync(like);

            return Result.Success(true);
        }
    }
}
