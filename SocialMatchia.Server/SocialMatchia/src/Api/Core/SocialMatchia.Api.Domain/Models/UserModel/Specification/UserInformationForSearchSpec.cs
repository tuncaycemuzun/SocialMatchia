﻿using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.UserModel.Specification
{
    public class UserInformationForSearchSpec : Specification<UserInformation>
    {
        public UserInformationForSearchSpec(Guid currentUserId, UserSetting setting, List<Guid> nonSearchableUserIdList)
        {
            if (!nonSearchableUserIdList.Any())
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
