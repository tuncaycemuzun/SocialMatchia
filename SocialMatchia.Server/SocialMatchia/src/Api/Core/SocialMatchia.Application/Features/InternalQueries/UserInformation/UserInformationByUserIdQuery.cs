using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.UserInformation
{
    public class UserInformationByUserIdQuery : IRequest<Domain.Models.UserInformationModel.UserInformation?>
    {
        public Guid UserId { get; set; }
    }

    public class UserInformationHandler : IRequestHandler<UserInformationByUserIdQuery, Domain.Models.UserInformationModel.UserInformation?>
    {
        private readonly IReadRepository<Domain.Models.UserInformationModel.UserInformation> _repository;

        public UserInformationHandler(IReadRepository<Domain.Models.UserInformationModel.UserInformation> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Domain.Models.UserInformationModel.UserInformation?> Handle(UserInformationByUserIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new GetUserInformationSpec(request.UserId), cancellationToken);
            return response;
        }
    }
}
