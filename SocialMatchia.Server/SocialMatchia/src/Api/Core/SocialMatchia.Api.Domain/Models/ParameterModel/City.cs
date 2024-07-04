namespace SocialMatchia.Domain.Models.ParameterModel
{
    public class City : BaseEntity
    {
        public required string Name { get; set; }
        public required Guid CountryId { get; set; }
        public required Country Country { get; set; }
    }
}
