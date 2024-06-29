namespace SocialMatchia.Common.Features.ResponseModel
{
    public class UserSearchModel
    {
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required List<string>? Photos { get; set; }
        public string? City { get; set; }
    }
}
