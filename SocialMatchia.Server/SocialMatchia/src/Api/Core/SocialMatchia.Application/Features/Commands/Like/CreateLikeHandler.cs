using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.Like
{
    public class CreateLikeHandler : IRequestHandler<CreateLikeCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.Like> _repository;
        private readonly CurrentUser _currentUser;

        public CreateLikeHandler(IRepositoryBase<Domain.Models.Like> repository, CurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var hasExistingLike = await _repository.AnyAsync(new LikeCheckHasExistSpec(request.TargetUserId), cancellationToken);

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
