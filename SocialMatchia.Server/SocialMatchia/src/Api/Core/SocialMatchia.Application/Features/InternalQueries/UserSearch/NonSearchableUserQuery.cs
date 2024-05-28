using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.LikeModel.Specifications;

namespace SocialMatchia.Application.Features.InternalQueries.UserSearch
{
    internal class NonSearchableUserQuery : IRequest<List<Guid>>
    {
        public Guid UserId { get; set; }
    }

    internal class NonSearchableUserQueryHandler : IRequestHandler<NonSearchableUserQuery, List<Guid>?>
    {
        private readonly IReadRepository<Domain.Models.LikeModel.Like> _repository;

        public NonSearchableUserQueryHandler(IReadRepository<Domain.Models.LikeModel.Like> repository)
        {
            _repository = repository;
        }

        public async Task<List<Guid>?> Handle(NonSearchableUserQuery request, CancellationToken cancellationToken)
        {
            var nonSearchableUsers = await _repository.ListAsync(new NonSearchableUserSpec(request.UserId), cancellationToken);
            var nonSearchableUserIds = nonSearchableUsers?.Select(x => x.TargetUserId).ToList();
            return nonSearchableUserIds;
        }
    }
}
