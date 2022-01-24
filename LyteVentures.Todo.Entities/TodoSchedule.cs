using System;

namespace LyteVentures.Todo.Entities
{
    public class TodoSchedule
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime StartSchedule { get; set; } = DateTime.Now;
        public DateTime EndSchedule { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
