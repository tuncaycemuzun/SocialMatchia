using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class LikeSpec : Specification<Like>
    {
        public LikeSpec(Guid targetUserId, Guid sourceUserId)
        {
            Query.Where(x => x.TargetUserId == targetUserId && x.SourceUserId == sourceUserId && x.IsDeleted == false);
        }
    }
}
