using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class UserPhotosSpec : Specification<UserPhoto>
    {
        public UserPhotosSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId && x.IsDeleted == false);
        }
    }
}
