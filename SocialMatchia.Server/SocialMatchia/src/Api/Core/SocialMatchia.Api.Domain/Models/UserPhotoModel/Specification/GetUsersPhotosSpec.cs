using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserPhotoModel.Specification
{
    public class GetUsersPhotosSpec : Specification<UserPhoto>
    {
        public GetUsersPhotosSpec(List<Guid> userIds)
        {
            Query.Where(x => userIds.Contains(x.UserId) && x.IsDeleted == false);
        }
    }
}
