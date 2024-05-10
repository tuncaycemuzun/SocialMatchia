using Ardalis.Result;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class DeleteUserPhotoCommand : IRequest<Result<bool>>
    {
        public required Guid Id { get; set; }
    }

    public class DeleteUserPhotoHandler : IRequestHandler<DeleteUserPhotoCommand, Result<bool>>
    {
        private readonly IRepository<Domain.Models.UserPhotoModel.UserPhoto> _repository;
        private readonly CurrentUser _currentUser;

        public DeleteUserPhotoHandler(IRepository<Domain.Models.UserPhotoModel.UserPhoto> repository, CurrentUser currentUser)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public async Task<Result<bool>> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _repository.FirstOrDefaultAsync(new GetUserPhotoSpec(request.Id, _currentUser.Id)) ?? throw new NotFoundException("Photo not found");

            photo.SetIsDeleted(false);

            await _repository.UpdateAsync(photo, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
