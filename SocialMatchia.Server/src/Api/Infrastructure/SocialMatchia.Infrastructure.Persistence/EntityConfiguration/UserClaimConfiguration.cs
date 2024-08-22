using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMatchia.Infrastructure.Persistence.EntityConfiguration
{
    internal class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
        {
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.ToTable("UserClaims");
        }
    }
}
