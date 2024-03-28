using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMatchia.Infrastructure.Persistence.Seeds;

namespace SocialMatchia.Infrastructure.Persistence.Context;

public class SocialMatchiaDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public SocialMatchiaDbContext(DbContextOptions<SocialMatchiaDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(SocialMatchiaDbContext).Assembly);

        builder.Seed();
    }
}

