using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

namespace SocialMatchia.Application.Features.ForAppQueries.UserInformation
{
    public class UserInformationGetCommand : IRequest<Domain.Models.UserInformation?>
    {
        public Guid UserId { get; set; }
    }

    public class UserInformationGetHandler : IRequestHandler<UserInformationGetCommand, Domain.Models.UserInformation?>
    {
        private readonly IReadRepositoryBase<Domain.Models.UserInformation> _repository;
        private readonly CurrentUser _currentUser;

        public UserInformationGetHandler(CurrentUser currentUser, IReadRepositoryBase<Domain.Models.UserInformation> repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public async Task<Domain.Models.UserInformation?> Handle(UserInformationGetCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new GetUserInformationSpec(request.UserId), cancellationToken);
            return response;
        }
    }
}
