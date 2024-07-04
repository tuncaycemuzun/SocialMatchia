﻿namespace SocialMatchia.Application.Features.Commands
{
    public class UpsertUserSocialMediaCommand : IRequest<Result<bool>>
    {
        public required Dictionary<Guid, string> Values { get; set; }
    }

    public class UpsertUserSocialMediaHandler : IRequestHandler<UpsertUserSocialMediaCommand, Result<bool>>
    {
        private readonly IRepository<UserSocialMedia> _userSocialMedia;
        private readonly IReadRepository<SocialMedia> _socialMediaRepository;
        private readonly CurrentUser _currentUser;

        public UpsertUserSocialMediaHandler(IRepository<UserSocialMedia> userSocialMedia, CurrentUser currentUser, IReadRepository<SocialMedia> socialMediaRepository)
        {
            _userSocialMedia = userSocialMedia ?? throw new ArgumentNullException(nameof(userSocialMedia));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task<Result<bool>> Handle(UpsertUserSocialMediaCommand request, CancellationToken cancellationToken)
        {
            if (request.Values.Count == 0)
            {
                throw new PropertyValidationException("Social Media required");
            }

            var socialMedias = await _socialMediaRepository.ListAsync(new SocialMediaSpec(), cancellationToken);

            foreach (var socialMedia in socialMedias!)
            {
                if (!request.Values.ContainsKey(socialMedia.Id))
                {
                    throw new PropertyValidationException("Invalid social media");
                }
            }

            var userSocialMedias = await _userSocialMedia.ListAsync(new UserSocialMediaSpec(_currentUser.Id), cancellationToken);

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
                    userSocialMedias.Add(new UserSocialMedia
                    {
                        UserId = _currentUser.Id,
                        SocialMediaId = item.Key,
                        UserName = item.Value
                    });
                }

                await _userSocialMedia.UpdateRangeAsync(userSocialMedias, cancellationToken);
            }
            else
            {
                var userSocialMedia = new List<UserSocialMedia>();

                foreach (var item in request.Values)
                {
                    userSocialMedia.Add(new UserSocialMedia
                    {
                        UserId = _currentUser.Id,
                        SocialMediaId = item.Key,
                        UserName = item.Value
                    });
                }

                await _userSocialMedia.AddRangeAsync(userSocialMedia);
            }

            await _userSocialMedia.SaveChangesAsync(cancellationToken);

            return Result<bool>.Error("Invalid social media");
        }
    }
}
