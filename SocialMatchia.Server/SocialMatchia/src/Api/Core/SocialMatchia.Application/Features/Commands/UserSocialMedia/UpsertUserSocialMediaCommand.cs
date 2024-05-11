using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Application.Features.InternalQueries.SocialMedia;
using SocialMatchia.Application.Features.InternalQueries.UserSocialMedia;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Interfaces;

namespace SocialMatchia.Application.Features.Commands.UserSocialMedia
{
    public class UpsertUserSocialMediaCommand : IRequest<Result<bool>>
    {
        public required Dictionary<Guid, string> Values { get; set; }
    }

    public class UpsertUserSocialMediaHandler : IRequestHandler<UpsertUserSocialMediaCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.UserSocialMediaModel.UserSocialMedia> _repository;
        private readonly CurrentUser _currentUser;
        private readonly IMediator _mediator;

        public UpsertUserSocialMediaHandler(IRepository<Domain.Models.UserSocialMediaModel.UserSocialMedia> repository, CurrentUser currentUser, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _mediator = mediator;
        }

        public async Task<Result<bool>> Handle(UpsertUserSocialMediaCommand request, CancellationToken cancellationToken)
        {
            if (request.Values.Count == 0)
            {
                throw new PropertyValidationException("Social Media required");
            }

            var socialMedias = await _mediator.Send(new SocialMediaQuery(), cancellationToken);

            foreach (var socialMedia in socialMedias!)
            {
                if (!request.Values.ContainsKey(socialMedia.Id))
                {
                    throw new PropertyValidationException("Invalid social media");
                }
            }

            var userSocialMedias = await _mediator.Send(new UserSocialMediaQuery(), cancellationToken);

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
                    userSocialMedias.Add(new Domain.Models.UserSocialMediaModel.UserSocialMedia
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
                var userSocialMedia = new List<Domain.Models.UserSocialMediaModel.UserSocialMedia>();

                foreach (var item in request.Values)
                {
                    userSocialMedia.Add(new Domain.Models.UserSocialMediaModel.UserSocialMedia
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
