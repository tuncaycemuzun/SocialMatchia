using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class GetUserSettingSpec : Specification<UserSetting>
    {
        public GetUserSettingSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
