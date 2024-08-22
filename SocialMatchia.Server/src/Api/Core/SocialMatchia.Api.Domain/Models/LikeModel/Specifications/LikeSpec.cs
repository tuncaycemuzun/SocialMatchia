using Ardalis.Specification;
using SocialMatchia.Domain.Models.LikeModel;

namespace SocialMatchia.Domain.Models.LikeModel.Specifications
{
    public class LikeSpec : Specification<Like>
    {
        public LikeSpec(Guid targetUserId, Guid sourceUserId)
        {
            Query.Where(x => x.TargetUserId == targetUserId && x.SourceUserId == sourceUserId && x.IsDeleted == false);
        }
    }
}
