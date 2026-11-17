
using EventHub.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Configurations;

public class OrganizerProfileConfig
    : IEntityTypeConfiguration<OrganizerProfile>
{
    public void Configure(EntityTypeBuilder<OrganizerProfile> builder)
    {
        builder.HasKey(p => p.OrganizerId);

        builder.HasOne(p => p.Organizer)
               .WithOne(o => o.Profile)
               .HasForeignKey<OrganizerProfile>(p => p.OrganizerId);
    }
}
