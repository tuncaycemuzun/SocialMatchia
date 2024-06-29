using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Features.InternalQueries.UserInformation;
using SocialMatchia.Common;
using SocialMatchia.Common.Interfaces;

namespace SocialMatchia.Application.Features.Commands.UserInformation
{
    public class UpsertUserInformationCommand : IRequest<Result<bool>>
    {
        public required Guid UserId { get; set; }
        public required Guid CityId { get; set; }
        public required string Bio { get; set; }
        public required string Website { get; set; }
        public required Guid GenderId { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }

    public class UpsertUserInformationHandler : IRequestHandler<UpsertUserInformationCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.UserInformationModel.UserInformation> _repository;
        private readonly CurrentUser _currentUser;
        private readonly IMediator _mediator;

        public UpsertUserInformationHandler(IRepository<Domain.Models.UserInformationModel.UserInformation> repository, CurrentUser currentUser, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result<bool>> Handle(UpsertUserInformationCommand request, CancellationToken cancellationToken)
        {
            var userInformation = await _mediator.Send(new UserInformationQuery()
            {
                UserId = _currentUser.Id
            }, cancellationToken);

            if (userInformation is not null)
            {
                userInformation.SetUserInformation(request.FirstName, request.LastName, request.CityId, request.Bio, request.Website, request.GenderId, request.BirthDate);
                await _repository.UpdateAsync(userInformation, cancellationToken);
            }
            else
            {
                await _repository.AddAsync(new Domain.Models.UserInformationModel.UserInformation
                {
                    UserId = request.UserId,
                    CityId = request.CityId,
                    Bio = request.Bio,
                    Website = request.Website,
                    GenderId = request.GenderId,
                    BirthDate = request.BirthDate,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                }, cancellationToken);
            }

            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
