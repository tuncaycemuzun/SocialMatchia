using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specification
{
    public class UserInformationByUserIdSpec : Specification<UserInformation>
    {
        public UserInformationByUserIdSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId).Include(x => x.City).Include(x => x.Gender);
        }
    }
}
