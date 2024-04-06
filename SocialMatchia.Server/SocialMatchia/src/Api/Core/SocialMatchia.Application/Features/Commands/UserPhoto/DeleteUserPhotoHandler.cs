using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class DeleteUserPhotoHandler : IRequestHandler<DeleteUserPhotoCommand, Result<bool>>
    {
        private readonly IRepositoryBase<Domain.Models.UserPhoto> _repository;
        private readonly CurrentUser _currentUser;

        public DeleteUserPhotoHandler(IRepositoryBase<Domain.Models.UserPhoto> repository, CurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _repository.FirstOrDefaultAsync(new GetPhotoSpec(request.Id, _currentUser.Id)) ?? throw new NotFoundException("Photo not found");

            photo.SetIsDeleted(false);

            await _repository.UpdateAsync(photo, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
