namespace WebChat.BLL.Services
{
    public class Result<T>
    {
        public T ResultData { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}
