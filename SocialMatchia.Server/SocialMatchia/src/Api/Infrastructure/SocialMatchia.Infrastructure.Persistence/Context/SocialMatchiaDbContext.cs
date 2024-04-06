using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMatchia.Common;
using SocialMatchia.Domain.Models;
using SocialMatchia.Infrastructure.Persistence.Seeds;

namespace SocialMatchia.Infrastructure.Persistence.Context;

public class SocialMatchiaDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly CurrentUser _currentUser;
    public SocialMatchiaDbContext(DbContextOptions<SocialMatchiaDbContext> options, CurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser;
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
            if (item.Entity is BaseDetailEntity entityReference)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreateDate = DateTime.UtcNow;
                            entityReference.CreatedUserId = _currentUser.Id;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entityReference.UpdateDate = DateTime.UtcNow;
                            entityReference.UpdatedUserId = _currentUser.Id;

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
            if (item.Entity is BaseDetailEntity entityReference)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreateDate = DateTime.Now;
                            entityReference.CreatedUserId = _currentUser.Id;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entityReference.UpdateDate = DateTime.Now;
                            entityReference.UpdatedUserId = _currentUser.Id;

                            break;
                        }
                }

            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

