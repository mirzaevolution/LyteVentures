using System;
using System.ComponentModel.DataAnnotations;

namespace LyteVentures.Todo.Api.Models
{
    public class ScheduleExistsRequest
    {
        [Required]
        public DateTime StartSchedule { get; set; }
        [Required]
        public DateTime EndSchedule { get; set; }
    }
}
