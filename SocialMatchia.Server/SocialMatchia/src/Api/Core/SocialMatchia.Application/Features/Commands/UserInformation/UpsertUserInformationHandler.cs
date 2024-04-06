using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Interfaces.Repositories;

namespace SocialMatchia.Application.Features.Commands.UserInformation
{
    public class UpsertUserInformationHandler : IRequestHandler<UpsertUserInformationCommand, Result<bool>>
    {
        private readonly IGenericRepository<Domain.Models.UserInformation> _repository;

        public UpsertUserInformationHandler(IGenericRepository<Domain.Models.UserInformation> repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(UpsertUserInformationCommand request, CancellationToken cancellationToken)
        {
            var userInformation = await _repository.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (userInformation is not null)
            {
                userInformation.SetUserInformation(request.CityId, request.Bio, request.Website, request.GenderId, request.BirthDate);
                await _repository.UpdateAsync(userInformation);
            }
            else
            {
                await _repository.AddAsync(new Domain.Models.UserInformation
                {
                    UserId = request.UserId,
                    CityId = request.CityId,
                    Bio = request.Bio,
                    Website = request.Website,
                    GenderId = request.GenderId,
                    BirthDate = request.BirthDate
                });
            }

            return Result.Success(true);
        }
    }
}
