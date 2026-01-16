using Microsoft.EntityFrameworkCore;
using BitbucketSystem.Data.Models; 

namespace BitbucketSystem.Data;

public class BitbucketContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Repository> Repositories { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Commit> Commits { get; set; }
    public DbSet<BitbucketSystem.Data.Models.File> Files { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=BitbucketDb;Integrated Security=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Repository>()
            .HasMany(r => r.Contributors)
            .WithMany(u => u.Repositories)
            .UsingEntity(j => j.ToTable("RepositoriesContributors"));

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Assignee)
            .WithMany(u => u.Issues)
            .HasForeignKey(i => i.AssigneeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}