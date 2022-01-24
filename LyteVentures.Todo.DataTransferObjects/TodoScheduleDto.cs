using System;

namespace LyteVentures.Todo.DataTransferObjects
{
    public class TodoScheduleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime StartSchedule { get; set; }
        public DateTime EndSchedule { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
    }
}
