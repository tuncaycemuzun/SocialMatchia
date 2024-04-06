using Ardalis.Result;
using MediatR;

namespace SocialMatchia.Application.Features.Commands.UserSocialMedia
{
    public class CreateUserSocialMediaCommand : IRequest<Result<bool>>
    {
        public required Dictionary<Guid, string> Values { get; set; }
    }
}
