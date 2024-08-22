using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMatchia.Infrastructure.Persistence.EntityConfiguration
{
    public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
        {
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.ToTable("RoleClaims");
        }
    }
}
