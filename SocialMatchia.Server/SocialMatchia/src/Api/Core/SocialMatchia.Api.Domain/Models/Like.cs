using Microsoft.AspNetCore.Identity;

namespace SocialMatchia.Domain.Models
{
    public class Like : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public bool SourceUserId { get; set; }
        public required IdentityUser<Guid> SourceUser { get; set; }
        public bool TargetUserId { get; set; }
        public required IdentityUser<Guid> TargetUser { get; set; }
    }
}
