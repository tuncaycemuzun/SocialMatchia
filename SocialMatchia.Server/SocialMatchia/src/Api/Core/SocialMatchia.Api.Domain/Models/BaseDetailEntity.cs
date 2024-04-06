namespace SocialMatchia.Domain.Models
{
    public class BaseDetailEntity : BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
}
