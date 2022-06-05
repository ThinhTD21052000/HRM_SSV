using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class WageConfig : IEntityTypeConfiguration<Wage>
    {
        public void Configure(EntityTypeBuilder<Wage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
            builder.HasOne(x => x.Wage_Type)
                .WithMany(x => x.Wages)
                .HasForeignKey(x => x.Wage_TypeId);
            builder.Property(x => x.Tax)
                .HasColumnType("DECIMAL(3,3)");
            builder.HasOne(x => x.MonthlySalary)
                .WithOne()
                .HasForeignKey<MonthlySalary>("MonthlySalaryId");
        }
    }
}
