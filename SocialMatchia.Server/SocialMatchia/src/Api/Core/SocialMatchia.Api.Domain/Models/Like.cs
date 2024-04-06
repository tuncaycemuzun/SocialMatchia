using Microsoft.AspNetCore.Identity;

namespace SocialMatchia.Domain.Models
{
    public class Like : BaseDetailEntity
    {
        public bool IsDeleted { get; set; }
        public Guid SourceUserId { get; set; }
        public User SourceUser { get; set; }
        public Guid TargetUserId { get; set; }
        public User TargetUser { get; set; }
    }
}
