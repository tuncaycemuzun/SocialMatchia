using MediatR;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.UserInformation
{
    internal class UserInformationQuery : IRequest<Domain.Models.UserInformationModel.UserInformation?>
    {
        public Guid UserId { get; set; }
    }

    internal class UserInformationHandler : IRequestHandler<UserInformationQuery, Domain.Models.UserInformationModel.UserInformation?>
    {
        private readonly IReadRepository<Domain.Models.UserInformationModel.UserInformation> _repository;

        public UserInformationHandler(IReadRepository<Domain.Models.UserInformationModel.UserInformation> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Domain.Models.UserInformationModel.UserInformation?> Handle(UserInformationQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new GetUserInformationByUserIdSpec(request.UserId), cancellationToken);
            return response;
        }
    }
}
