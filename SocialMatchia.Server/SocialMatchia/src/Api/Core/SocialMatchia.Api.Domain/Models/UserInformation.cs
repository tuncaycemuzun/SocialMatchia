using Microsoft.AspNetCore.Identity;

namespace SocialMatchia.Domain.Models
{
    public class UserInformation : BaseDetailEntity
    {
        public required Guid UserId { get; set; }
        public required IdentityUser<Guid> User { get; set; }
        public required Guid CityId { get; set; }
        public required City City { get; set; }
        public required Guid TownId { get; set; }
        public required Town Town { get; set; }
        public required Guid CountryId { get; set; }
        public required Country Country { get; set; }
        public required string Bio { get; set; }
        public required string Website { get; set; }
        public required Guid GenderId { get; set; }
        public required Gender Gender { get; set; }
        public required DateTime BirthDate { get; set; }

        public ICollection<UserSocialMedia> SocialMedias { get; set; }
    }
}
