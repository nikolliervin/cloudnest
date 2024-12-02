using CloudNest.Api.Helpers;
using CloudNest.Api.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CloudNest.Api.Models;
using Directory = CloudNest.Api.Models.Directory;



public class ApplicationDbContext : IdentityDbContext<User, Role, string>
{
    private readonly UserHelper _userContext;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, UserHelper userContext)
        : base(options)
    {
        _userContext = userContext;
    }

    public DbSet<Directory> Directories { get; set; }
    public DbSet<DirectoryShare> DirectoryShares { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DirectoryShare>()
            .HasKey(ds => new { ds.DirectoryId, ds.UserId });

        modelBuilder.Entity<DirectoryShare>()
                .HasKey(ds => new { ds.DirectoryId, ds.UserId });

        modelBuilder.Entity<DirectoryShare>()
            .HasOne(ds => ds.Directory)
            .WithMany(d => d.DirectoryShares)
            .HasForeignKey(ds => ds.DirectoryId)
            .OnDelete(DeleteBehavior.Cascade);


    }
    public override int SaveChanges()
    {
        SetAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditEntry && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

        var currentUserId = _userContext.GetCurrentUserId();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((IAuditEntry)entry.Entity).CreatedAt = DateTime.UtcNow;
                ((IAuditEntry)entry.Entity).CreatedBy = currentUserId;
            }

            if (entry.State == EntityState.Modified)
            {
                ((IAuditEntry)entry.Entity).UpdatedAt = DateTime.UtcNow;
                ((IAuditEntry)entry.Entity).UpdatedBy = currentUserId;
            }

            if (entry.State == EntityState.Deleted)
            {
                ((IAuditEntry)entry.Entity).Deleted = true;
                ((IAuditEntry)entry.Entity).DeletedAt = DateTime.UtcNow;
                ((IAuditEntry)entry.Entity).DeletedBy = currentUserId;
                entry.State = EntityState.Modified;
            }
        }
    }

}

