using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class MonthlySalaryConfig : IEntityTypeConfiguration<MonthlySalary>
    {
        public void Configure(EntityTypeBuilder<MonthlySalary> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
            builder.Property(x => x.UserId).HasMaxLength(200);
        }
    }
}
