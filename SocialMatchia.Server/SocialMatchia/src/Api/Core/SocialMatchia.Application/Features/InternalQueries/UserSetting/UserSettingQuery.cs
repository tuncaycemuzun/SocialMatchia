using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserSettingModel.Specifications;

namespace SocialMatchia.Application.Features.InternalQueries.UserSetting
{
    internal class UserSettingQuery : IRequest<Domain.Models.UserSettingModel.UserSetting?>
    {
        public Guid UserId { get; set; }
    }

    internal class UserSettingHandler : IRequestHandler<UserSettingQuery, Domain.Models.UserSettingModel.UserSetting?>
    {
        private readonly IReadRepository<Domain.Models.UserSettingModel.UserSetting> _repository;

        public UserSettingHandler(IReadRepository<Domain.Models.UserSettingModel.UserSetting> repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Models.UserSettingModel.UserSetting?> Handle(UserSettingQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FirstOrDefaultAsync(new GetUserSettingSpec(request.UserId), cancellationToken);
        }
    }
}
