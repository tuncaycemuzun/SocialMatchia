namespace SocialMatchia.Common.Features.ResponseModel
{
    public class SocialMediaResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string IconName { get; set; }
        public int Order { get; set; }
    }
}
