using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserInformationModel.Specification
{
    public class GetUserInformationByUserIdsSpec : Specification<UserInformation>
    {
        public GetUserInformationByUserIdsSpec(List<Guid> userIds)
        {
            Query.Where(x => userIds.Contains(x.UserId)).Include(x => x.City);
        }
    }
}
