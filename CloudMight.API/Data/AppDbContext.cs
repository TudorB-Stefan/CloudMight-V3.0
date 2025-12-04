using CloudMight.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = CloudMight.API.Entities.File;

namespace CloudMight.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User> (options)
{
    public DbSet<Partition> Partitions { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<File> Files { get; set; }

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
        modelBuilder.Entity<Partition>(partition => 
        {
            partition.HasOne(p => p.User)
                .WithMany(u => u.Partitions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            partition.HasOne(p => p.MainFolder)
                .WithOne(f => f.Partition!)
                .HasForeignKey<Partition>(p => p.MainFolderId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Folder>(folder =>
        {
            folder.HasOne(f => f.ParentFolder)
                .WithMany(f => f.Folders)
                .HasForeignKey(f => f.ParentFolderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            folder.HasOne(f => f.Partition)
                .WithOne(p => p.MainFolder)
                .HasForeignKey<Folder>(p => p.PartitionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<File>(file =>
        {
            file.HasOne(f => f.Folder)
                .WithMany(folder => folder.Files)
                .HasForeignKey(f => f.FolderId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}