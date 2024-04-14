using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Domain.Models.UserSocialMediaModel.Specifications;

namespace SocialMatchia.Application.Features.Queries.UserSocialMedia
{
    public class UserSocialMediaQuery : IRequest<Result<UserSocialMediaResponse>>
    {
    }
    public class UserSocialMediaHandler(IReadRepositoryBase<Domain.Models.UserSocialMedia> repository, CurrentUser currentUser) : IRequestHandler<UserSocialMediaQuery, Result<UserSocialMediaResponse>>
    {
        private readonly IReadRepositoryBase<Domain.Models.UserSocialMedia> _repository = repository;
        private readonly CurrentUser _currentUser = currentUser;
        
        public async Task<Result<UserSocialMediaResponse>> Handle(UserSocialMediaQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new GetUserSocialMediaSpec(_currentUser.Id), cancellationToken);
            if (response == null)
            {
                return Result<UserSocialMediaResponse>.NotFound();
            }

            return Result<UserSocialMediaResponse>.Success(new UserSocialMediaResponse
            {
                SocialMediaId = response.SocialMediaId,
                UserName = response.UserName
            });
        }
    }
}
