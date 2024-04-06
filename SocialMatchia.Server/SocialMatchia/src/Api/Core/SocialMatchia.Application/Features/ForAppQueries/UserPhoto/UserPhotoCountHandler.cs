using Ardalis.Specification;
using MediatR;
using SocialMatchia.Domain.Models.UserPhotoModel.Specification;

namespace SocialMatchia.Application.Features.ForAppQueries.UserPhoto
{
    public class UserPhotoGetCommand : IRequest<int>
    {
        public Guid UserId{ get; set; }
    }

    public class UserPhotoCountHandler : IRequestHandler<UserPhotoGetCommand, int>
    {
        private readonly IRepositoryBase<Domain.Models.UserPhoto> _repository;

        public UserPhotoCountHandler(IRepositoryBase<Domain.Models.UserPhoto> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(UserPhotoGetCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.CountAsync(new GetCurrentUserPhotoSpec(request.UserId));
            return response;
        }
    }
}
