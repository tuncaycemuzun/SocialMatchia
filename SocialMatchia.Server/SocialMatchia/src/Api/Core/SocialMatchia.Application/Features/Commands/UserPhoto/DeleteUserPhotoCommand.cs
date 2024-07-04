using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models;
using SocialMatchia.Domain.Models.Specifications;

namespace SocialMatchia.Application.Features.Commands
{
    public class DeleteUserPhotoCommand : IRequest<Result<bool>>
    {
        public required Guid Id { get; set; }
    }

    public class DeleteUserPhotoHandler : IRequestHandler<DeleteUserPhotoCommand, Result<bool>>
    {
        private readonly IRepository<UserPhoto> _userPhoto;
        private readonly CurrentUser _currentUser;

        public DeleteUserPhotoHandler(IRepository<UserPhoto> userPhoto, CurrentUser currentUser)
        {
            _userPhoto = userPhoto ?? throw new ArgumentNullException(nameof(userPhoto));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<bool>> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _userPhoto.FirstOrDefaultAsync(new GetUserPhotosSpec(_currentUser.Id), cancellationToken);

            if (photo is null) return Result.Success(false);

            photo.SetIsDeleted(true);

            await _userPhoto.UpdateAsync(photo, cancellationToken);
            await _userPhoto.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
