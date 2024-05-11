using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Domain.Models.TownModel
{
    public class Town : BaseEntity
    {
        public required string Name { get; set; }
        public required Guid CityId { get; set; }
        public required City City { get; set; }
    }
}
