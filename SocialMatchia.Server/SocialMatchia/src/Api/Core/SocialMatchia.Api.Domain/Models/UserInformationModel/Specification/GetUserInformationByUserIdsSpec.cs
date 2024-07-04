using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class GetUserInformationByUserIdsSpec : Specification<UserInformation>
    {
        public GetUserInformationByUserIdsSpec(List<Guid> userIds)
        {
            Query.Where(x => userIds.Contains(x.UserId)).Include(x => x.City);
        }
    }
}
