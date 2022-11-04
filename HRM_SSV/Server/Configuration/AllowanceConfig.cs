using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class AllowanceConfig : IEntityTypeConfiguration<Allowance>
    {
        public void Configure(EntityTypeBuilder<Allowance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
