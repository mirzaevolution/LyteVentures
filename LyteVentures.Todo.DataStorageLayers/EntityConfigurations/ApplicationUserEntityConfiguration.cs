using LyteVentures.Todo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LyteVentures.Todo.DataStorageLayers.EntityConfigurations
{
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(c => c.FullName).HasMaxLength(256).IsRequired();
            builder.HasMany(c => c.TodoSchedules).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
