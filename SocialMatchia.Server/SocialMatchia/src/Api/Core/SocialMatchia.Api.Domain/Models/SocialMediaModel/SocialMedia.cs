namespace SocialMatchia.Domain.Models.SocialMediaModel
{
    public class SocialMedia : BaseEntity
    {
        public required string Name { get; set; }
        public required string IconName { get; set; }
        public int Order { get; set; }
    }
}
