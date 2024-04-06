using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Domain.Models.SocialMediaModel.Specifications;
using SocialMatchia.Domain.Models.UserSocialMediaModel.Specifications;

namespace SocialMatchia.Application.Features.Commands.UserSocialMedia
{
    public class CreateUserSocialMediaCommand : IRequest<Result<bool>>
    {
        public required Dictionary<Guid, string> Values { get; set; }
    }

    public class CreateUserSocialMediaHandler : IRequestHandler<CreateUserSocialMediaCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.UserSocialMedia> _repository;
        private readonly IRepositoryBase<Domain.Models.SocialMedia> _socialMediaRepository;
        private readonly CurrentUser _currentUser;

        public CreateUserSocialMediaHandler(IRepositoryBase<Domain.Models.UserSocialMedia> repository, IRepositoryBase<Domain.Models.SocialMedia> socialMediaRepository, CurrentUser currentUser)
        {
            _repository = repository;
            _socialMediaRepository = socialMediaRepository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(CreateUserSocialMediaCommand request, CancellationToken cancellationToken)
        {
            var socialMedias = await _socialMediaRepository.ListAsync(new GetAllSocialMediaSpec(), cancellationToken);

            if (request.Values.Count == 0)
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

            var userSocialMedias = await _repository.ListAsync(new GetUserSocialMedia(_currentUser.Id), cancellationToken);

            if (userSocialMedias.Count > 0)
            {
                var existMedias = userSocialMedias.Where(x => request.Values.Select(x => x.Key).Contains(x.Id)).ToList();

                foreach (var item in existMedias)
                {
                    item.UserName = request.Values.FirstOrDefault(x => x.Key == item.Id).Value;
                }

                var newSocialMedias = request.Values.Where(x => !existMedias.Select(x => x.SocialMediaId).Contains(x.Key)).ToList();

                foreach (var item in newSocialMedias)
                {
                    userSocialMedias.Add(new Domain.Models.UserSocialMedia
                    {
                        UserId = _currentUser.Id,
                        SocialMediaId = item.Key,
                        UserName = item.Value
                    });
                }

                await _repository.UpdateRangeAsync(userSocialMedias, cancellationToken);
            }
            else
            {
                var userSocialMedia = new List<Domain.Models.UserSocialMedia>();

                foreach (var item in request.Values)
                {
                    userSocialMedia.Add(new Domain.Models.UserSocialMedia
                    {
                        UserId = _currentUser.Id,
                        SocialMediaId = item.Key,
                        UserName = item.Value
                    });
                }

                await _repository.AddRangeAsync(userSocialMedia);
            }

            await _repository.SaveChangesAsync(cancellationToken);

            return Result<bool>.Error("Invalid social media");
        }
    }
}
