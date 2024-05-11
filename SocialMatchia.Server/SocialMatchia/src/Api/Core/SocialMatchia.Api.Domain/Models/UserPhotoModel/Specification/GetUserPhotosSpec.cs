using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserPhotoModel.Specification
{
    public class GetUserPhotosSpec : Specification<UserPhoto>
    {
        public GetUserPhotosSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId && x.IsDeleted == false);
        }
    }
}
