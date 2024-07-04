using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specification
{
    public class UserPhotoSpec : Specification<UserPhoto>
    {
        public UserPhotoSpec(Guid userId, Guid id)
        {
            Query.Where(x => x.UserId == userId && x.Id == id && x.IsDeleted == false);
        }
    }
}
