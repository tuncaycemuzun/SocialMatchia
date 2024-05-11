using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserInformationModel.Specification
{
    public class GetUserInformationByUserIdSpec : Specification<UserInformation>
    {
        public GetUserInformationByUserIdSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
