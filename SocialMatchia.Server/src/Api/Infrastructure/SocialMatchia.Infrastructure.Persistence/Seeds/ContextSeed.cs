using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMatchia.Common;

namespace SocialMatchia.Infrastructure.Persistence.Seeds
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            CreateRoles(modelBuilder);
        }

        public static void CreateRoles(ModelBuilder modelBuilder)
        {

            var admin = Enum.GetName(typeof(Enums.Roles), Enums.Roles.Admin)!;
            var basic = Enum.GetName(typeof(Enums.Roles), Enums.Roles.Basic)!;

            var roles = new List<IdentityRole<Guid>>()
            {
                new IdentityRole<Guid>
                {
                    Id = Consts.Admin,
                    Name = admin,
                    NormalizedName = admin.ToUpper()
                },
                new IdentityRole<Guid>
                {
                    Id = Consts.Basic,
                    Name = basic,
                    NormalizedName = basic.ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(roles);
        }
    }
}
