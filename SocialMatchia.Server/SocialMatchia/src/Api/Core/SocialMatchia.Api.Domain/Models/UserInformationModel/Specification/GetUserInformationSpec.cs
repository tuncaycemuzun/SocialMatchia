using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserInformationModel.Specification
{
    public class GetUserInformationSpec : Specification<UserInformation>
    {
        public GetUserInformationSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
