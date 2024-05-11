using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserPhotoModel.Specification
{
    public class GetUserPhotoSpecByPhotoId : Specification<UserPhoto>
    {
        public GetUserPhotoSpecByPhotoId(Guid id, Guid userId)
        {
            Query.Where(x => x.Id == id && x.UserId == userId && x.IsDeleted == false);
        }
    }
}
