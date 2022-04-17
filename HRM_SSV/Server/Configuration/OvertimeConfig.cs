using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class OvertimeConfig : IEntityTypeConfiguration<Overtime>
    {
        public void Configure(EntityTypeBuilder<Overtime> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
        }
    }
}
