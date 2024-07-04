using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specification
{
    public class UserSocialMediaSpec : Specification<UserSocialMedia>
    {
        public UserSocialMediaSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
