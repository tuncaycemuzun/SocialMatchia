using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserModel.Specification
{
    public class UserSettingSpec : Specification<UserSetting>
    {
        public UserSettingSpec(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
