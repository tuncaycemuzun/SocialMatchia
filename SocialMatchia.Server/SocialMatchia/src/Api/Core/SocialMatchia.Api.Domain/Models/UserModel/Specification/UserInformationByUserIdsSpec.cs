using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specification
{
    public class UserInformationByUserIdsSpec : Specification<UserInformation>
    {
        public UserInformationByUserIdsSpec(List<Guid> userIds)
        {
            Query.Where(x => userIds.Contains(x.UserId)).Include(x => x.City);
        }
    }
}
