using System;
using System.ComponentModel.DataAnnotations;

namespace LyteVentures.Todo.Api.Models
{
    public class UpdateTodoRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartSchedule { get; set; }
        [Required]
        public DateTime EndSchedule { get; set; }
    }
}
