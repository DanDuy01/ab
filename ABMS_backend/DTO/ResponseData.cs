using System.Net;

namespace ABMS_backend.DTO
{
    public class ResponseData<T>
    {
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrMsg { get; set; }
    }
}
