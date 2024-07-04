using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{ 
    public class GetUserSocialMediaSpec : Specification<UserSocialMedia>
    {
        public GetUserSocialMediaSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);   
        }
    }
}
