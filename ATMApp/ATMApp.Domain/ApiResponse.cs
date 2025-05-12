using System.Net;

namespace ATMApp.Domain
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }
        public ApiResponse(T? data,
            string message,
            List<string> errors,
            bool isSuccessful)
        {
            Data = data;
            Message = message;
            Errors = errors;
            IsSuccessful = isSuccessful;
        }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsSuccessful { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
