using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMatchia.Domain.Models;

namespace SocialMatchia.Infrastructure.Persistence.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.ToTable("Users");
        }
    }
}
