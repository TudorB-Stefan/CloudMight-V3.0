using CloudMight.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = CloudMight.API.Entities.File;

namespace CloudMight.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User> (options)
{
    public DbSet<File> Files { get; set; }
    public DbSet<Partition> Partitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Id = "member-id",
                    Name = "Member",
                    NormalizedName = "MEMBER"
                },
                new IdentityRole
                {
                    Id = "admin-id",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        modelBuilder.Entity<User>()
            .HasOne(u => u.Partition)
            .WithOne(p => p.User)
            .HasForeignKey<Partition>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Partition>()
            .HasMany(p => p.Files)
            .WithOne(f => f.Partition)
            .HasForeignKey(f => f.PartitionId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<File>()
            .HasIndex(f => new { f.PartitionId, f.Name });
    }
}