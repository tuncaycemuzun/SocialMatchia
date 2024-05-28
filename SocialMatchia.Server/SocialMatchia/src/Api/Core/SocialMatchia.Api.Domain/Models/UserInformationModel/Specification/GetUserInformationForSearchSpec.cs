using Ardalis.Specification;
using SocialMatchia.Domain.Models.UserSettingModel;

namespace SocialMatchia.Domain.Models.UserInformationModel.Specification
{
    public class GetUserInformationForSearchSpec : Specification<UserInformation>
    {
        public GetUserInformationForSearchSpec(Guid currentUserId, UserSetting setting, List<Guid>? nonSearchableUserIdList)
        {
            if (nonSearchableUserIdList is null)
            {
                Query.Where(
                x => x.UserId != currentUserId &&
                x.CityId == setting.CityId &&
                x.GenderId == setting.GenderId &&
                x.BirthDate >= DateTime.Now.AddYears(setting.EndAge * -1) && x.BirthDate <= DateTime.Now.AddYears(setting.BeginAge * -1));
            }
            else
            {
                Query.Where(
                    x => x.UserId != currentUserId &&
                    x.CityId == setting.CityId &&
                    x.GenderId == setting.GenderId &&
                    x.BirthDate >= DateTime.Now.AddYears(setting.EndAge * -1) && x.BirthDate <= DateTime.Now.AddYears(setting.BeginAge * -1) &&
                    !nonSearchableUserIdList.Contains(x.UserId));
            }
        }
    }
}
