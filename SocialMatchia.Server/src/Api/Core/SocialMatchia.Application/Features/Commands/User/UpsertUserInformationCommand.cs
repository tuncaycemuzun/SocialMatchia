using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserModel.Specification;

namespace SocialMatchia.Application.Features.Commands.User
{
    public class UpsertUserInformationCommand : IRequest<Result<bool>>
    {
        public required Guid CityId { get; set; }
        public required string Bio { get; set; }
        public required string Website { get; set; }
        public required Guid GenderId { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }

    public class UpsertUserInformationHandler(IRepository<Domain.Models.UserModel.User> userInformation, CurrentUser currentUser) : IRequestHandler<UpsertUserInformationCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.UserModel.User> _user = userInformation ?? throw new ArgumentNullException(nameof(userInformation));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<bool>> Handle(UpsertUserInformationCommand request, CancellationToken cancellationToken)
        {
            var userInformation = await _user.FirstOrDefaultAsync(new UserInformationByUserIdSpec(_currentUser.Id), cancellationToken);

            if (userInformation is null) return Result.NotFound();

            userInformation.SetUserInformation(request.FirstName, request.LastName, request.CityId, request.Bio, request.Website, request.GenderId, request.BirthDate);
            await _user.UpdateAsync(userInformation, cancellationToken);

            await _user.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
