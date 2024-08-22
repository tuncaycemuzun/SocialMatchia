using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Application.Features.Queries.Parameter
{
    public class GenderQuery : IRequest<Result<List<GenderResponse>>>
    {
    }

    public class GenderHandler(IReadRepository<Gender> gender) : IRequestHandler<GenderQuery, Result<List<GenderResponse>>>
    {
        private readonly IReadRepository<Gender> _gender = gender ?? throw new ArgumentNullException(nameof(gender));

        public async Task<Result<List<GenderResponse>>> Handle(GenderQuery request, CancellationToken cancellationToken)
        {
            var genders = await _gender.ListAsync(cancellationToken);

            var response = genders.Select(x => new GenderResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Result.Success(response);
        }
    }
}
