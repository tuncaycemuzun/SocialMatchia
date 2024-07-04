using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class GetUserPhotosSpec : Specification<UserPhoto>
    {
        public GetUserPhotosSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId && x.IsDeleted == false);
        }
    }
}
