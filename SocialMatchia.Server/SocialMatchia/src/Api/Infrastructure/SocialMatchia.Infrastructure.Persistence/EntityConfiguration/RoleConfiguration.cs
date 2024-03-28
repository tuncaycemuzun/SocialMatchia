using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMatchia.Infrastructure.Persistence.EntityConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.ToTable("Roles");
        }
    }
}
