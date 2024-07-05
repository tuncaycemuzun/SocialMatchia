using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserModel.Specification
{
    public class UserInformationByUserIdSpec : Specification<User>
    {
        public UserInformationByUserIdSpec(Guid userId)
        {
            Query.Where(x => x.Id == userId).Include(x => x.City).Include(x => x.Gender);
        }
    }
}
