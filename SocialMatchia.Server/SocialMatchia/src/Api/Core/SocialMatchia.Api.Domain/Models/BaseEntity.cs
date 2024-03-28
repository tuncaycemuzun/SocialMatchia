namespace SocialMatchia.Domain.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime CreateDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
}
