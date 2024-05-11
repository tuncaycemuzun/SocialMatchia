using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Features.InternalQueries.UserPhoto;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Interfaces;

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
        private readonly IMediator _mediator;

        public DeleteUserPhotoHandler(IRepository<Domain.Models.UserPhotoModel.UserPhoto> repository, CurrentUser currentUser, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result<bool>> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _mediator.Send(new UserPhotoQuery
            {
                UserId = _currentUser.Id,
            }) ?? throw new NotFoundException("Photo not found");

            photo.SetIsDeleted(true);

            await _repository.UpdateAsync(photo, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
