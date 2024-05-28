﻿using SocialMatchia.Domain.Models.ParameterModel;
using SocialMatchia.Domain.Models.UserModel;

namespace SocialMatchia.Domain.Models.UserSocialMediaModel
{
    public class UserSocialMedia : BaseDetailEntity
    {
        public required Guid UserId { get; set; }
        public User User { get; set; }
        public required Guid SocialMediaId { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public required string UserName { get; set; }
    }
}