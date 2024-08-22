using SocialMatchia.Domain.Models.UserModel;

namespace SocialMatchia.Domain.Models.LikeModel
{
    public class Like : BaseDetailEntity
    {
        public bool IsDeleted { get; set; } = false;
        public Guid SourceUserId { get; set; }
        public User SourceUser { get; set; }
        public Guid TargetUserId { get; set; }
        public User TargetUser { get; set; }

        public void SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}
