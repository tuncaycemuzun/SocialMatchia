using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

namespace SocialMatchia.Application.Features.Queries.UserInformation
{
    public class UserInformationQuery : IRequest<Result<UserInformationResponse>>
    {

    }

    public class UserInformationHandler : IRequestHandler<UserInformationQuery, Result<UserInformationResponse>>
    {
        private readonly IReadRepository<Domain.Models.UserInformationModel.UserInformation> _repository;
        private readonly CurrentUser _currentUser;

        public UserInformationHandler(IReadRepository<Domain.Models.UserInformationModel.UserInformation> repository, CurrentUser currentUser)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<UserInformationResponse>> Handle(UserInformationQuery request, CancellationToken cancellationToken)
        {
            var userInformation = await _repository.FirstOrDefaultAsync(new GetUserInformationSpec(_currentUser.Id), cancellationToken);

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
