using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class CreateLikeHandler : IRequestHandler<CreateLikeCommand, Result<bool>>
    {
        private readonly IGenericRepository<Domain.Models.Like> _repository;
        private readonly CurrentUser _currentUser;

        public CreateLikeHandler(IGenericRepository<Domain.Models.Like> repository, CurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var hasExistingLike = await _repository
                .AnyAsync(x => x.TargetUserId == request.TargetUserId && x.SourceUserId == _currentUser.Id && x.IsDeleted == false);

            if (hasExistingLike) return Result.Success(true);

            var like = new Domain.Models.Like
            {
                SourceUserId = _currentUser.Id,
                TargetUserId = request.TargetUserId,
            };

            await _repository.AddAsync(like);
            //TODO: Send notification to the target user

            return Result.Success(true);
        }
    }
}
