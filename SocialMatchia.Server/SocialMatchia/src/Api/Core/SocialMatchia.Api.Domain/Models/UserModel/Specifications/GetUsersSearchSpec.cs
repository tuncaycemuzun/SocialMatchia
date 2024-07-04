using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class GetUsersSearchSpec : Specification<User>
    {
        public GetUsersSearchSpec(List<Guid> userIds)
        {
            Query.Where(x => userIds.Contains(x.Id));
        }
    }
}
