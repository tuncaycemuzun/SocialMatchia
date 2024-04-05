namespace SocialMatchia.Domain.Models
{
    public class UserSetting
    {
        public Guid Id { get; set; } = new Guid();
        public required int BeginAge { get; set; } = 18;
        public int? EndAge { get; set; }
        public Guid? CityId { get; set; }
        public City? City { get; set; }
        public Guid? GenderId { get; set; }
        public Gender? Gender { get; set; }
    }
}
