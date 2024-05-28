using SocialMatchia.Domain.Models.ParameterModel;
using SocialMatchia.Domain.Models.UserModel;

namespace SocialMatchia.Domain.Models.UserSettingModel
{
    public class UserSetting : BaseEntity
    {
        public required int BeginAge { get; set; }
        public required int EndAge { get; set; }
        public required Guid CityId { get; set; }
        public required City City { get; set; }
        public required Guid GenderId { get; set; }
        public required Gender Gender { get; set; }
        public required Guid UserId { get; set; }
        public required User User { get; set; }

        public void SetUserSetting(UserSetting userSetting)
        {
            BeginAge = userSetting.BeginAge;
            EndAge = userSetting.EndAge;
            CityId = userSetting.CityId;
            GenderId = userSetting.GenderId;
            Gender = userSetting.Gender;
        }
    }
}
