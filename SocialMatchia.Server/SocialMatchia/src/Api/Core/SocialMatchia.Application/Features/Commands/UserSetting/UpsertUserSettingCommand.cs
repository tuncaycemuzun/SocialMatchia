using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Features.InternalQueries.UserSetting;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserSettingModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.UserSetting
{
    public class UpsertUserSettingCommand : IRequest<Result<bool>>
    {
        public int? BeginAge { get; set; }
        public int? EndAge { get; set; }
        public Guid? CityId { get; set; }
        public Guid? GenderId { get; set; }
    }

    public class UpsertUserSettingHandler : IRequestHandler<UpsertUserSettingCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.UserSettingModel.UserSetting> _repository;
        private readonly CurrentUser _currentUser;
        private readonly IMediator _mediator;

        public UpsertUserSettingHandler(IRepository<Domain.Models.UserSettingModel.UserSetting> repository, CurrentUser currentUser, IMediator mediator)
        {
            _repository = repository;
            _currentUser = currentUser;
            _mediator = mediator;
        }

        public async Task<Result<bool>> Handle(UpsertUserSettingCommand request, CancellationToken cancellationToken)
        {
            var userSettings = await _mediator.Send(new UserSettingQuery
            {
                UserId = _currentUser.Id
            });

            var update = userSettings != null ? true : false;

            userSettings ??= new Domain.Models.UserSettingModel.UserSetting { UserId = _currentUser.Id };

            userSettings.SetUserSetting(userSettings);

            if (update)
            {
                await _repository.AddAsync(userSettings, cancellationToken);
            }
            else
            {
                await _repository.UpdateAsync(userSettings, cancellationToken);
            }

            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
