using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.LikeModel.Specifications
{
    public class NonSearchableUserSpec : Specification<Like>
    {
        public NonSearchableUserSpec(Guid userId)
        {
            var date = DateTime.Now.AddMonths(-1);
            Query.Where(x => x.SourceUserId == userId && x.CreateDate >= date);
        }
    }
}
