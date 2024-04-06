using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserPhotoModel.Specification
{
    public class GetCurrentUserPhotoSpec : Specification<UserPhoto>
    {
        public GetCurrentUserPhotoSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId && x.IsDeleted == false);
        }
    }
}
