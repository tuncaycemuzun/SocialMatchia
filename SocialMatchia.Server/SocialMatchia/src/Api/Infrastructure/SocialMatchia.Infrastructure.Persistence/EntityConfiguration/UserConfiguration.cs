using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMatchia.Infrastructure.Persistence.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<IdentityUser<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUser<Guid>> builder)
        {
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.ToTable("Users");
        }
    }
}
