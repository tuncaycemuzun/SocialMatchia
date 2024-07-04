using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Domain.Models.UserModel
{
    public class UserInformation : BaseDetailEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
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

        public void SetUserInformation(Guid userId, string firstName, string lastName, Guid cityId, string bio, string website, Guid genderId, DateTime birthDate)
        {
            UserId = userId;
            CityId = cityId;
            Bio = bio;
            Website = website;
            GenderId = genderId;
            BirthDate = birthDate;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
