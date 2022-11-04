﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities;

namespace Server.Configuration
{
    public class Bonus_WageConfig : IEntityTypeConfiguration<Bonus_Wage>
    {
        public void Configure(EntityTypeBuilder<Bonus_Wage> builder)
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
