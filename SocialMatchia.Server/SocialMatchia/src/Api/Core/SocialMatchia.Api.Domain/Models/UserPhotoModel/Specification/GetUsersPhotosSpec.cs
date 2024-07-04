using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class GetUsersPhotosSpec : Specification<UserPhoto>
    {
        public GetUsersPhotosSpec(List<Guid> userIds)
        {
            Query.Where(x => userIds.Contains(x.UserId) && x.IsDeleted == false);
        }
    }
}
