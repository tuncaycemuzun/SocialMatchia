namespace SocialMatchia.Domain.Models
{
    public class SocialMedia : BaseEntity
    {
        public required string Name { get; set; }
        public required string IconPath { get; set; }
    }
}
