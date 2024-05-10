using Ardalis.Specification;

namespace SocialMatchia.Common.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
    }
}
