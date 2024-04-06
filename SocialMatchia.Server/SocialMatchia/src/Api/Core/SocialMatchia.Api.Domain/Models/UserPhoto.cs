using Microsoft.AspNetCore.Identity;

namespace SocialMatchia.Domain.Models
{
    public class UserPhoto : BaseEntity
    {
        public required Guid UserId { get; set; }
        public IdentityUser<Guid> User { get; set; }
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
        public int Order { get; set; }
    }
}
