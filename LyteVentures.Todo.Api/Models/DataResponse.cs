namespace LyteVentures.Todo.Api.Models
{
    public class DataResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
