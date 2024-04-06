﻿using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models.UserInformationModel.Specification;

namespace SocialMatchia.Application.Features.Commands.UserInformation
{
    public class UpsertUserInformationHandler : IRequestHandler<UpsertUserInformationCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.UserInformation> _repository;
        private readonly CurrentUser _currentUser;

        public UpsertUserInformationHandler(IRepositoryBase<Domain.Models.UserInformation> repository, CurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(UpsertUserInformationCommand request, CancellationToken cancellationToken)
        {
            var userInformation = await _repository.FirstOrDefaultAsync(new GetUserInformationSpec(_currentUser.Id), cancellationToken);

            if (userInformation is not null)
            {
                userInformation.SetUserInformation(request.CityId, request.Bio, request.Website, request.GenderId, request.BirthDate);
                await _repository.UpdateAsync(userInformation, cancellationToken);
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
                }, cancellationToken);
            }

            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
