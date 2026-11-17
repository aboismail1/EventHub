
using EventHub.Configurations;
using EventHub.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EventHub.Data;

public class EventHubContext : DbContext
{
    public DbSet<Organizer> Organizers { get; set; }

    public DbSet<OrganizerProfile> OrganizerProfiles { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Attendee> Attendees { get; set; }

    public DbSet<Badge> Badges { get; set; }


    protected override void OnConfiguring(
    DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=EventHubDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Separate Configuration Classes
        modelBuilder.ApplyConfiguration(new OrganizerProfileConfig());
        modelBuilder.ApplyConfiguration(new EventConfig());

        // Badge Fluent API - Direct Configuration
        modelBuilder.Entity<Badge>(builder =>
        {
            builder.HasKey(b => b.BadgeId);

            builder.Property(b => b.BadgeNumber)
                   .IsRequired();

            builder.HasIndex(b => b.BadgeNumber)
                   .IsUnique();

            builder.HasOne(b => b.Attendee)
                   .WithOne(a => a.Badge)
                   .HasForeignKey<Badge>(b => b.AttendeeId);
        });
    }
}
