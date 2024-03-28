namespace SocialMatchia.Domain.Models
{
    public class Town
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required Guid CityId { get; set; }
        public required City City { get; set; }
    }
}
