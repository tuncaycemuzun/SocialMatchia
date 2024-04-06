using Microsoft.AspNetCore.Identity;

namespace SocialMatchia.Domain.Models
{
    public class UserSetting : BaseEntity
    {
        public int? BeginAge { get; set; }
        public int? EndAge { get; set; }
        public Guid? CityId { get; set; }
        public City? City { get; set; }
        public Guid? GenderId { get; set; }
        public Gender? Gender { get; set; }
        public required Guid UserId { get; set; }
        public User User { get; set; }

        public void SetUserSetting(UserSetting? userSetting)
        {
            BeginAge = userSetting?.BeginAge == null ? 18 : userSetting.BeginAge;
            EndAge = userSetting?.EndAge;
            CityId = userSetting?.CityId;
            GenderId = userSetting?.GenderId;
            Gender = userSetting?.Gender;
        }
    }
}
