using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserSettingModel.Specifications
{
    public class GetUserSetting : Specification<UserSetting>
    {
        public GetUserSetting(Guid userId)
        {
            Query.Where(x => x.UserId == userId);
        }
    }
}
