using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class TimekeepingConfig : IEntityTypeConfiguration<Timekeeping>
    {
        public void Configure(EntityTypeBuilder<Timekeeping> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
            builder.Property(x => x.UserId).HasMaxLength(200);
        }
    }
}
