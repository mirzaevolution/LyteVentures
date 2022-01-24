using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LyteVentures.Todo.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public virtual List<TodoSchedule> TodoSchedules { get; set; } = new List<TodoSchedule>();
    }
}
