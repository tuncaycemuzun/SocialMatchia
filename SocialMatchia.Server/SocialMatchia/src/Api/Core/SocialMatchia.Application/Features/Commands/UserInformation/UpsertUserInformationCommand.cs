using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

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
        private readonly IRepository<Domain.Models.UserInformationModel.UserInformation> _userInformation;
        private readonly CurrentUser _currentUser;

        public UpsertUserInformationHandler(IRepository<Domain.Models.UserInformationModel.UserInformation> userInformation, CurrentUser currentUser)
        {
            _userInformation = userInformation ?? throw new ArgumentNullException(nameof(userInformation));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<bool>> Handle(UpsertUserInformationCommand request, CancellationToken cancellationToken)
        {
            var userInformation = await _userInformation.FirstOrDefaultAsync(new GetUserInformationByUserIdSpec(_currentUser.Id), cancellationToken);

            if (userInformation is not null)
            {
                userInformation.SetUserInformation(request.FirstName, request.LastName, request.CityId, request.Bio, request.Website, request.GenderId, request.BirthDate);
                await _userInformation.UpdateAsync(userInformation, cancellationToken);
            }
            else
            {
                await _userInformation.AddAsync(new Domain.Models.UserInformationModel.UserInformation
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

            await _userInformation.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
