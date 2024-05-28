using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.UserInformation
{
    internal class UserIdsByUserInformationSearchQuery : IRequest<List<Guid>>
    {
        public Guid CurrentUserId { get; set; }
        public Domain.Models.UserSettingModel.UserSetting UserSetting { get; set; }
        public List<Guid>? NonSearchableUserIdList { get; set; }
    }

    internal class UserIdsByUserInformationSearchHandler: IRequestHandler<UserIdsByUserInformationSearchQuery, List<Guid>>
    {
        private readonly IReadRepository<Domain.Models.UserInformationModel.UserInformation> _repository;
        private readonly IMediator _mediator;

        public UserIdsByUserInformationSearchHandler(IReadRepository<Domain.Models.UserInformationModel.UserInformation> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<List<Guid>> Handle(UserIdsByUserInformationSearchQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.ListAsync(new GetUserInformationForSearchSpec(request.CurrentUserId, request.UserSetting, request.NonSearchableUserIdList), cancellationToken);
            return users.Select(x => x.UserId).ToList();
        }
    }
}
