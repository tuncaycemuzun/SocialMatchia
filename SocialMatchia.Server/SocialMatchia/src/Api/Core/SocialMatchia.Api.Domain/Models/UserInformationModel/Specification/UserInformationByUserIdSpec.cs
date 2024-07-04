using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class UserInformationByUserIdSpec : Specification<UserInformation>
    {
        public UserInformationByUserIdSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
