﻿namespace SocialMatchia.Domain.Models.UserModel
{
    public class UserPhoto : BaseDetailEntity
    {
        public required Guid UserId { get; set; }
        public User User { get; set; }
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
        public int Order { get; set; }
        public bool IsDeleted { get; set; } = false;

        public void SetIsDeleted(bool value)
        {
            IsDeleted = value;
        }
    }
}
