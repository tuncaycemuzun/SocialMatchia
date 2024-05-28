﻿using SocialMatchia.Domain.Models.ParameterModel;
using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Domain.Models.UserSocialMediaModel;

namespace SocialMatchia.Domain.Models.UserInformationModel
{
    public class UserInformation : BaseDetailEntity
    {
        public required Guid UserId { get; set; }
        public User User { get; set; }
        public required Guid CityId { get; set; }
        public City City { get; set; }
        //public required Guid TownId { get; set; }
        //public Town Town { get; set; }
        //public required Guid CountryId { get; set; }
        //public Country Country { get; set; }
        public required string Bio { get; set; }
        public required string Website { get; set; }
        public required Guid GenderId { get; set; }
        public Gender Gender { get; set; }
        public required DateTime BirthDate { get; set; }

        public ICollection<UserSocialMedia> SocialMedias { get; set; }

        public void SetUserInformation(Guid cityId, string bio, string website, Guid genderId, DateTime birthDate)
        {
            CityId = cityId;
            Bio = bio;
            Website = website;
            GenderId = genderId;
            BirthDate = birthDate;
        }
    }
}