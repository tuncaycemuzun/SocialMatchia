using Ardalis.Result;
using MediatR;

namespace SocialMatchia.Application.Features.Commands.UserInformation
{
    public class UpsertUserInformationCommand : IRequest<Result<bool>>
    {
        public required Guid UserId { get; set; }
        public required Guid CityId { get; set; }
        public required string Bio { get; set; }
        public required string Website { get; set; }
        public required Guid GenderId { get; set; }
        public required DateTime BirthDate { get; set; }
    }
}
