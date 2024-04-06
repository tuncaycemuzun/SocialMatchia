using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserPhotoModel.Specification
{
    public class GetUserPhotoSpec: Specification<UserPhoto>
    {
        public GetUserPhotoSpec(Guid id, Guid userId)
        {
            Query.Where(x => x.Id == id && x.UserId == userId && x.IsDeleted == false);
        }
    }
}
