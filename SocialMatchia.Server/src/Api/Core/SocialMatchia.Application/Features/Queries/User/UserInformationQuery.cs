using SocialMatchia.Domain.Models.UserModel.Specification;

namespace SocialMatchia.Application.Features.Queries.User
{
    public class UserInformationQuery : IRequest<Result<UserInformationResponse>>
    {
        public Guid? UserId { get; set; }
    }

    public class UserInformationHandler(IReadRepository<Domain.Models.UserModel.User> user, CurrentUser currentUser) : IRequestHandler<UserInformationQuery, Result<UserInformationResponse>>
    {
        private readonly IReadRepository<Domain.Models.UserModel.User> _user = user ?? throw new ArgumentNullException(nameof(user));
        private readonly CurrentUser _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        public async Task<Result<UserInformationResponse>> Handle(UserInformationQuery request, CancellationToken cancellationToken)
        {
            var userInformation = await _user.FirstOrDefaultAsync(new UserInformationByUserIdSpec(request.UserId ?? _currentUser.Id), cancellationToken);

            if (userInformation is null)
            {
                return Result.Error("User information not found");
            }

            var response = new UserInformationResponse
            {
                UserId = userInformation.Id,
                CityId = userInformation.CityId!.Value,
                Bio = userInformation.Bio!,
                Website = userInformation.Website!,
                GenderId = userInformation.GenderId!.Value,
                BirthDate = userInformation.BirthDate,
                FirstName = userInformation.FirstName!,
                LastName = userInformation.LastName!,
                CityName = userInformation.City.Name,
                GenderName = userInformation.Gender.Name
            };

            return Result.Success(response);
        }
    }
}
