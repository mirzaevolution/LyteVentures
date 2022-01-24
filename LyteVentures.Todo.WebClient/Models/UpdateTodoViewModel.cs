using System;
using System.ComponentModel.DataAnnotations;


namespace LyteVentures.Todo.WebClient.Models
{
    public class UpdateTodoViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Start Schedule")]

        public DateTime StartSchedule { get; set; }
        [Required]
        [Display(Name = "End Schedule")]


        public DateTime EndSchedule { get; set; }
    }
}
