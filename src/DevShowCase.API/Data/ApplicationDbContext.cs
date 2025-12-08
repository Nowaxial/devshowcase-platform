using DevShowCase.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Education> Education => Set<Education>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<License> Licenses => Set<License>();
    public DbSet<Competency> Competencies => Set<Competency>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<TechStack> TechStacks => Set<TechStack>();
    public DbSet<Theme> Themes => Set<Theme>();
    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<UserAISettings> UserAISettings => Set<UserAISettings>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships

        // User -> Experience (One-to-Many)
        modelBuilder.Entity<Experience>()
            .HasOne(e => e.User)
            .WithMany(u => u.Experiences)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Project (One-to-Many)
        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Education (One-to-Many)
        modelBuilder.Entity<Education>()
            .HasOne(e => e.User)
            .WithMany(u => u.Education)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Skill (One-to-Many)
        modelBuilder.Entity<Skill>()
            .HasOne(s => s.User)
            .WithMany(u => u.Skills)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> License (One-to-Many)
        modelBuilder.Entity<License>()
            .HasOne(l => l.User)
            .WithMany(u => u.Licenses)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Competency (One-to-Many)
        modelBuilder.Entity<Competency>()
            .HasOne(c => c.User)
            .WithMany(u => u.Competencies)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Language (One-to-Many)
        modelBuilder.Entity<Language>()
            .HasOne(l => l.User)
            .WithMany(u => u.Languages)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> TechStack (One-to-Many)
        modelBuilder.Entity<TechStack>()
            .HasOne(t => t.User)
            .WithMany(u => u.TechStacks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> ContactMessage (One-to-Many)
        modelBuilder.Entity<ContactMessage>()
            .HasOne(c => c.User)
            .WithMany(u => u.ContactMessages)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Theme (Many-to-One, optional)
        modelBuilder.Entity<User>()
            .HasOne(u => u.SelectedTheme)
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.SelectedThemeId)
            .OnDelete(DeleteBehavior.SetNull);

        // User -> UserAISettings (One-to-One)
        modelBuilder.Entity<UserAISettings>()
            .HasOne(s => s.User)
            .WithOne(u => u.AISettings)
            .HasForeignKey<UserAISettings>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> UserPreference (One-to-Many)
        modelBuilder.Entity<UserPreference>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for performance
        modelBuilder.Entity<Experience>()
            .HasIndex(e => e.UserId);

        modelBuilder.Entity<Project>()
            .HasIndex(p => p.UserId);

        modelBuilder.Entity<ContactMessage>()
            .HasIndex(c => new { c.UserId, c.CreatedAt });
    }
}
