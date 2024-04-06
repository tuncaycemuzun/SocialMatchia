using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Application.Features.ForAppQueries.UserInformation;
using SocialMatchia.Common;

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
    }

    public class UpsertUserInformationHandler : IRequestHandler<UpsertUserInformationCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.UserInformation> _repository;
        private readonly CurrentUser _currentUser;
        private readonly IMediator _mediator;

        public UpsertUserInformationHandler(IRepositoryBase<Domain.Models.UserInformation> repository, CurrentUser currentUser, IMediator mediator)
        {
            _repository = repository;
            _currentUser = currentUser;
            _mediator = mediator;
        }

        public async Task<Result<bool>> Handle(UpsertUserInformationCommand request, CancellationToken cancellationToken)
        {
            var userInformation = await _mediator.Send(new UserInformationGetCommand()
            {
                UserId = _currentUser.Id
            }, cancellationToken);

            if (userInformation is not null)
            {
                userInformation.SetUserInformation(request.CityId, request.Bio, request.Website, request.GenderId, request.BirthDate);
                await _repository.UpdateAsync(userInformation, cancellationToken);
            }
            else
            {
                await _repository.AddAsync(new Domain.Models.UserInformation
                {
                    UserId = request.UserId,
                    CityId = request.CityId,
                    Bio = request.Bio,
                    Website = request.Website,
                    GenderId = request.GenderId,
                    BirthDate = request.BirthDate
                }, cancellationToken);
            }

            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
