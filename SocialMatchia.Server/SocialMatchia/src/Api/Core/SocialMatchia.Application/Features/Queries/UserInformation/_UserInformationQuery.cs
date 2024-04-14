using Ardalis.Specification;
using MediatR;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

namespace SocialMatchia.Application.Features.Queries.UserInformation
{
    public class _UserInformationQuery : IRequest<Domain.Models.UserInformation?>
    {
        public Guid UserId { get; set; }
    }

    public class _UserInformationHandler(IReadRepositoryBase<Domain.Models.UserInformation> repository) : IRequestHandler<_UserInformationQuery, Domain.Models.UserInformation?>
    {
        private readonly IReadRepositoryBase<Domain.Models.UserInformation> _repository = repository;

        public async Task<Domain.Models.UserInformation?> Handle(_UserInformationQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FirstOrDefaultAsync(new GetUserInformationSpec(request.UserId), cancellationToken);
            return response;
        }
    }
}
