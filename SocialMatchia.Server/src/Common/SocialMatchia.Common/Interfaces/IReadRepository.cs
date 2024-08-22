using Ardalis.Specification;
using SocialMatchia.Common.Features.ResponseModel;

namespace SocialMatchia.Common.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
        Task<PaginationModel<T>> PagedListAsync(ISpecification<T> specification, int pageNumber, CancellationToken cancellationToken = default);
    }
}
