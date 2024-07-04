namespace SocialMatchia.Application.Features.Queries
{
    public class UserInformationQuery : IRequest<Result<UserInformationResponse>>
    {

    }

    public class UserInformationHandler : IRequestHandler<UserInformationQuery, Result<UserInformationResponse>>
    {
        private readonly IReadRepository<UserInformation> _userInformation;
        private readonly CurrentUser _currentUser;

        public UserInformationHandler(IReadRepository<UserInformation> userInformation, CurrentUser currentUser)
        {
            _userInformation = userInformation ?? throw new ArgumentNullException(nameof(userInformation));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<UserInformationResponse>> Handle(UserInformationQuery request, CancellationToken cancellationToken)
        {
            var userInformation = await _userInformation.FirstOrDefaultAsync(new GetUserInformationByUserIdSpec(_currentUser.Id), cancellationToken);

            if (userInformation is null)
            {
                return Result.Error("User information not found");
            }

            var response = new UserInformationResponse
            {
                UserId = userInformation.UserId,
                CityId = userInformation.CityId,
                Bio = userInformation.Bio,
                Website = userInformation.Website,
                GenderId = userInformation.GenderId,
                BirthDate = userInformation.BirthDate
            };

            return Result.Success(response);
        }
    }
}
