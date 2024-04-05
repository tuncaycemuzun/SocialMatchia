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
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<UserSetting> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(SocialMatchiaDbContext).Assembly);

        builder.Seed();
    }

    public override int SaveChanges()
    {
        var entriesCopy = ChangeTracker.Entries().ToList();

        foreach (var item in entriesCopy)
        {
            if (item.Entity is BaseEntity entityReference)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreateDate = DateTime.UtcNow;
                            entityReference.CreatedUserId = new Guid("ab750336-f152-46cb-b2d2-1e520711d361");
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entityReference.UpdateDate = DateTime.UtcNow;
                            entityReference.UpdatedUserId = new Guid("ab750336-f152-46cb-b2d2-1e520711d361");

                            break;
                        }
                }
            }
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entriesCopy = ChangeTracker.Entries().ToList();

        foreach (var item in entriesCopy)
        {
            if (item.Entity is BaseEntity entityReference)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreateDate = DateTime.Now;
                            entityReference.CreatedUserId = new Guid("ab750336-f152-46cb-b2d2-1e520711d361");
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entityReference.UpdateDate = DateTime.Now;
                            entityReference.UpdatedUserId = new Guid("ab750336-f152-46cb-b2d2-1e520711d361");

                            break;
                        }
                }

            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

