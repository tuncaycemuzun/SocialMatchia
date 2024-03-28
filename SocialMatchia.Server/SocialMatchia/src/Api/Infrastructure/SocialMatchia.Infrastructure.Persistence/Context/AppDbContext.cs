using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMatchia.Domain.Models;
using SocialMatchia.Infrastructure.Persistence.Seeds;

namespace SocialMatchia.Infrastructure.Persistence.Context;

public class SocialMatchiaDbContext : IdentityDbContext<User, Role, Guid>
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

