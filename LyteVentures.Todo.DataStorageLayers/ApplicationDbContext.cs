using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LyteVentures.Todo.Entities;
using LyteVentures.Todo.DataStorageLayers.EntityConfigurations;

namespace LyteVentures.Todo.DataStorageLayers
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
            builder.ApplyConfiguration(new TodoScheduleEntityConfiguration());
        }
        public virtual DbSet<TodoSchedule> TodoSchedules { get; set; }
    }
}
