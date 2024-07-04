using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specification
{
    public class UsersPhotoSpec : Specification<UserPhoto>
    {
        public UsersPhotoSpec(List<Guid> userIds)
        {
            Query.Where(x => userIds.Contains(x.UserId) && x.IsDeleted == false);
        }
    }
}
