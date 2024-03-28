using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMatchia.Domain.Models;
using SocialMatchia.Infrastructure.Persistence.Seeds;

namespace SocialMatchia.Infrastructure.Persistence.Context;

public class SocialMatchiaDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public SocialMatchiaDbContext(DbContextOptions<SocialMatchiaDbContext> options) : base(options)
    {

    }

    public DbSet<UserPhoto> UserPhotos { get; set; }
    public DbSet<UserSocialMedia> UserSocialMedias { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Town> Towns { get; set; }
    public DbSet<UserInformation> UserInformations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(SocialMatchiaDbContext).Assembly);

        builder.Seed();
    }
}

