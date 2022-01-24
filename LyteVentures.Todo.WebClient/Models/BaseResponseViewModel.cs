namespace LyteVentures.Todo.WebClient.Models
{
    public class BaseResponseViewModel
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
