using LyteVentures.Todo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LyteVentures.Todo.DataStorageLayers.EntityConfigurations
{
    public class TodoScheduleEntityConfiguration : IEntityTypeConfiguration<TodoSchedule>
    {
        public void Configure(EntityTypeBuilder<TodoSchedule> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(2000);
            builder.Property(c => c.StartSchedule).HasConversion(dt => dt, dt => dt.AddTicks(-dt.Ticks % TimeSpan.TicksPerSecond));
            builder.Property(c => c.EndSchedule).HasConversion(dt => dt, dt => dt.AddTicks(-dt.Ticks % TimeSpan.TicksPerSecond));

        }
    }
}
