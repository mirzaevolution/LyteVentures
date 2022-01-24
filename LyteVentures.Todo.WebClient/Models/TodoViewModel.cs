using System;

namespace LyteVentures.Todo.WebClient.Models
{
    public class TodoViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime StartSchedule { get; set; }
        public DateTime EndSchedule { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
    }
}
