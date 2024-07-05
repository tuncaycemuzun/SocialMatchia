using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserModel.Specification
{
    public class UserInformationByUserIdsSpec : Specification<User>
    {
        public UserInformationByUserIdsSpec(List<Guid> userIds)
        {
            Query.Where(x => userIds.Contains(x.Id)).Include(x => x.City);
        }
    }
}
