using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.LikeModel.Specifications
{
    public class LikeCheckHasExistSpec : Specification<Like>
    {
        public LikeCheckHasExistSpec(Guid targetUserId, Guid sourceUserId)
        {
            Query.Where(x => x.TargetUserId == targetUserId && x.SourceUserId == sourceUserId && x.IsDeleted == false);
        }

        public LikeCheckHasExistSpec(Guid targetUserId)
        {
            Query.Where(x => x.TargetUserId == targetUserId && x.IsDeleted == false);
        }
    }
}
