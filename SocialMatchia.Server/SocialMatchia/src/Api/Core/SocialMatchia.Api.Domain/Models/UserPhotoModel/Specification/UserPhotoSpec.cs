using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class UserPhotoSpec : Specification<UserPhoto>
    {
        public UserPhotoSpec(Guid userId, Guid id)
        {
            Query.Where(x => x.UserId == userId && x.Id == id && x.IsDeleted == false);
        }
    }
}
