using Ardalis.Result;
using Microsoft.AspNetCore.Identity;

namespace SocialMatchia.Application.Features.Commands.User
{
    public class CreateUserCommand : IRequest<Result<bool>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Guid CityId { get; set; }
        public required string Bio { get; set; }
        public required string Website { get; set; }
        public required Guid GenderId { get; set; }
        public required DateTime BirthDate { get; set; }
    }

    public class CreateUserHandler(UserManager<Domain.Models.UserModel.User> userManager) : IRequestHandler<CreateUserCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await userManager.CreateAsync(new Domain.Models.UserModel.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email.Split("@")[0],
                Bio = request.Bio,
                BirthDate = request.BirthDate,
                CityId = request.CityId,
                GenderId = request.GenderId,
                Website = request.Website
            }, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                throw new PropertyValidationException(string.Join("##", errors));
            }

            return Result.Success();
        }
    }
}
