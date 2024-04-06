using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.LikeModel.Specifications
{
    public class LikeGetSpec : Specification<Like>
    {
        public LikeGetSpec(Guid targetUserId, Guid sourceUserId)
        {
            Query.Where(x => x.TargetUserId == targetUserId && x.SourceUserId == sourceUserId && x.IsDeleted == false);
        }

        public LikeGetSpec(Guid targetUserId)
        {
            Query.Where(x => x.TargetUserId == targetUserId && x.IsDeleted == false);
        }
    }
}
