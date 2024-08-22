using Microsoft.AspNetCore.Identity;
using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Domain.Models.UserModel
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }
        //public required Guid TownId { get; set; }
        //public Town Town { get; set; }
        //public required Guid CountryId { get; set; }
        //public Country Country { get; set; }
        public string? Bio { get; set; }
        public string? Website { get; set; }
        public Guid? GenderId { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<UserSocialMedia> SocialMedias { get; set; }

        public void SetUserInformation(string firstName, string lastName, Guid cityId, string bio, string website, Guid genderId, DateTime birthDate)
        {
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
