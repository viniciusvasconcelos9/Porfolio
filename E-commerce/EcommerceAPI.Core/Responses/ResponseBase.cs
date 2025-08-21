namespace EcommerceAPI.Core.Responses
{
    public class ResponseBase<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ResponseBase()
        {
            Errors = new List<string>();
        }
    }
}

