using Ardalis.Result;
using MediatR;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Common.Exceptions;

namespace SocialMatchia.Application.Features.Commands.UserPhoto
{
    public class DeleteUserPhotoHandler : IRequestHandler<DeleteUserPhotoCommand, Result<bool>>
    {
        private readonly IGenericRepository<Domain.Models.UserPhoto> _repository;

        public DeleteUserPhotoHandler(IGenericRepository<Domain.Models.UserPhoto> repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new NotFoundException("Photo not found");

            photo.SetIsDeleted(false);
            await _repository.UpdateAsync(photo);

            return Result.Success(true);
        }
    }
}
