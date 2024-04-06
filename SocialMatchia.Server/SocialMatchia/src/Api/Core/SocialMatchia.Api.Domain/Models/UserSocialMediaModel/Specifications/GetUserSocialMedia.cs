using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserSocialMediaModel.Specifications
{
    public class GetUserSocialMedia : Specification<UserSocialMedia>
    {
        public GetUserSocialMedia(Guid userId)
        {
            Query.Where(x => x.UserId == userId);   
        }
    }
}
