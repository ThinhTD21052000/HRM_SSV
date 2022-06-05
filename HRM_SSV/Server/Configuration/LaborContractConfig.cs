using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class LaborContractConfig : IEntityTypeConfiguration<LaborContract>
    {
        public void Configure(EntityTypeBuilder<LaborContract> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.UserId).HasMaxLength(200);
        }
    }
}
