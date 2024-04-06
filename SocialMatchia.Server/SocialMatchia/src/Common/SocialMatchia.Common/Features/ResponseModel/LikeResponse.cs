namespace SocialMatchia.Common.Features.ResponseModel
{
    public class LikeResponse
    {
        public bool IsDeleted { get; set; } = false;
        public Guid SourceUserId { get; set; }
        public Guid TargetUserId { get; set; }
    }
}
