using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserPhotoModel.Specification
{
    public class GetUserPhotoSpec : Specification<UserPhoto>
    {
        public GetUserPhotoSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId && x.IsDeleted == false);
        }
    }
}
