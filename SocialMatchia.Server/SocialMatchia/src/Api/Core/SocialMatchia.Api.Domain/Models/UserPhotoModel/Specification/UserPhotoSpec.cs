using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class UserPhotoSpec : Specification<UserPhoto>
    {
        public UserPhotoSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId && x.IsDeleted == false);
        }
    }
}
