using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(x => x.LastName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(x => x.Address)
                .HasMaxLength(250);
            builder.HasOne(x => x.Team)
                .WithMany(x => x.User)
                .HasForeignKey("TeamId")
                .IsRequired(false);
            builder.HasOne(x => x.Position)
                .WithMany(x => x.User)
                .HasForeignKey("PositionId")
                .IsRequired(false);
        }
    }
}
