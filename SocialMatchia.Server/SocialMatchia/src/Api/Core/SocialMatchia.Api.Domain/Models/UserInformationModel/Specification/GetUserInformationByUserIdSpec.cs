using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class GetUserInformationByUserIdSpec : Specification<UserInformation>
    {
        public GetUserInformationByUserIdSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
