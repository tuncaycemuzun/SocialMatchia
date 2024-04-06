using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;

namespace SocialMatchia.Application.Features.Commands.UserSocialMedia
{
    public class CreateUserSocialMediaHandler : IRequestHandler<CreateUserSocialMediaCommand, Result<bool>>
    {
        private readonly IGenericRepository<Domain.Models.UserSocialMedia> _repository;
        private readonly IGenericRepository<Domain.Models.SocialMedia> _socialMediaRepository;
        private readonly CurrentUser _currentUser;

        public CreateUserSocialMediaHandler(IGenericRepository<Domain.Models.UserSocialMedia> repository, IGenericRepository<Domain.Models.SocialMedia> socialMediaRepository, CurrentUser currentUser)
        {
            _repository = repository;
            _socialMediaRepository = socialMediaRepository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(CreateUserSocialMediaCommand request, CancellationToken cancellationToken)
        {
            var socialMedias = await _socialMediaRepository.GetAllAsync(null, true, x => x.OrderByDescending(a => a.Order));

            if(request.Values.Count == 0)
            {
                throw new PropertyValidationException("Social Media required");
            }

            foreach (var socialMedia in socialMedias!)
            {
                if (!request.Values.ContainsKey(socialMedia.Id))
                {
                    throw new PropertyValidationException("Invalid social media");
                }
            }

            var userSocialMedias = await _repository.GetAllAsync(x => x.UserId == _currentUser.Id) ?? [];

            if(userSocialMedias.Count > 0)
            {
                await _repository.DeleteRangeAsync(userSocialMedias);
            }

            foreach (var item in request.Values)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    userSocialMedias.Add(new Domain.Models.UserSocialMedia
                    {
                        UserId = _currentUser.Id,
                        SocialMediaId = item.Key,
                        UserName = item.Value
                    });
                }
            }

            if(userSocialMedias.Count > 0)
            {
                await _repository.AddRangeAsync(userSocialMedias);
                return Result.Success(true);
            }

            return Result<bool>.Error("Invalid social media");
        }
    }
}
