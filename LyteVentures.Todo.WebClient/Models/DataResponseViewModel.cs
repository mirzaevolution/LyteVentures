namespace LyteVentures.Todo.WebClient.Models
{
    public class DataResponseViewModel<T>:BaseResponseViewModel
    {
        public T Data { get; set; }

    }
}
