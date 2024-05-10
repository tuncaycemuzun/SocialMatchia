using Ardalis.Specification;

namespace SocialMatchia.Common.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
