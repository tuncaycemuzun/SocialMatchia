using Microsoft.AspNetCore.Identity;

namespace SocialMatchia.Domain.Models
{
    public class UserSocialMedia : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required IdentityUser<Guid> User { get; set; }
        public required Guid SocialMediaId { get; set; }
        public required SocialMedia SocialMedia { get; set; }
    }
}
