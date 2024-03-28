namespace SocialMatchia.Domain.Models
{
    public class SocialMedia
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string IconName { get; set; }
        public int Order { get; set; }
    }
}
