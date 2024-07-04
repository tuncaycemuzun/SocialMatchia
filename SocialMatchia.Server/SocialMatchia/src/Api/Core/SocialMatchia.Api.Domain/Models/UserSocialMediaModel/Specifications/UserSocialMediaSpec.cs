using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{ 
    public class UserSocialMediaSpec : Specification<UserSocialMedia>
    {
        public UserSocialMediaSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);   
        }
    }
}
