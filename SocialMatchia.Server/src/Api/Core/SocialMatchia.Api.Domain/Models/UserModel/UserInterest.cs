using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Domain.Models.UserModel
{
    public class UserInterest : BaseDetailEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid InterestId { get; set; }
        public Interest Interest { get;set; }
    }
}
