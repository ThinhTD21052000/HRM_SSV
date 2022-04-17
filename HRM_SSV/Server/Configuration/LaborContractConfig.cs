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
            builder.HasOne(x => x.User)
                .WithMany(x => x.LaborContracts)
                .HasForeignKey(x => x.UserId);
        }
    }
}
