namespace SocialMatchia.Domain.Models.ParameterModel
{
    public class SocialMedia : BaseEntity
    {
        public required string Name { get; set; }
        public required string IconName { get; set; }
        public int Order { get; set; }
    }
}
