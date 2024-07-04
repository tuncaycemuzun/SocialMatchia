using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class UserSettingSpec : Specification<UserSetting>
    {
        public UserSettingSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
