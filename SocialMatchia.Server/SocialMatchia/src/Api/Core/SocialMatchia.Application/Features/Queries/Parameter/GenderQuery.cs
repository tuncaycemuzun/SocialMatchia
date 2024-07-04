using Ardalis.Result;
using MediatR;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models;

namespace SocialMatchia.Application.Features.Queries
{
    public class GenderQuery : IRequest<Result<List<GenderResponse>>>
    {
    }

    public class GenderHandler : IRequestHandler<GenderQuery, Result<List<GenderResponse>>>
    {
        private readonly IReadRepository<Gender> _gender;

        public GenderHandler(IReadRepository<Gender> gender)
        {
            _gender = gender ?? throw new ArgumentNullException(nameof(gender));
        }

        public async Task<Result<List<GenderResponse>>> Handle(GenderQuery request, CancellationToken cancellationToken)
        {
            var genders = await _gender.ListAsync();
            
            var response = genders.Select(x => new GenderResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Result.Success(response);
        }
    }
}
