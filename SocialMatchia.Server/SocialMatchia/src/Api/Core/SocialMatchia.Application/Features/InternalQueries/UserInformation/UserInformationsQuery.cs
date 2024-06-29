using MediatR;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

namespace SocialMatchia.Application.Features.InternalQueries.UserInformation
{
    internal class UserInformationsQuery : IRequest<PaginationModel<Domain.Models.UserInformationModel.UserInformation>>
    {
        public required List<Guid> UserIds { get; set; }
        public required int PageNumber { get; set; }
    }

    internal class UserInformationsHandler : IRequestHandler<UserInformationsQuery, PaginationModel<Domain.Models.UserInformationModel.UserInformation>>
    {
        private readonly IReadRepository<Domain.Models.UserInformationModel.UserInformation> _repository;

        public UserInformationsHandler(IReadRepository<Domain.Models.UserInformationModel.UserInformation> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<PaginationModel<Domain.Models.UserInformationModel.UserInformation>> Handle(UserInformationsQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.PagedListAsync(new GetUserInformationByUserIdsSpec(request.UserIds), request.PageNumber, cancellationToken);
            return response;
        }
    }

}
