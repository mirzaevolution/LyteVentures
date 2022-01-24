using System;
using System.ComponentModel.DataAnnotations;

namespace LyteVentures.Todo.Api.Models
{
    public class InsertTodoRequest
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartSchedule { get; set; }
        [Required]
        public DateTime EndSchedule { get; set; }
    }
}
