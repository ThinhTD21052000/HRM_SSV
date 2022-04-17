using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class Wage_TypeConfig : IEntityTypeConfiguration<Wage_Type>
    {
        public void Configure(EntityTypeBuilder<Wage_Type> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
            builder.Property(x => x.Title)
                .HasMaxLength(200);
            builder.HasOne(x => x.Position)
                .WithOne()
                .HasForeignKey<Position>("PositionId");
        }
    }
}
