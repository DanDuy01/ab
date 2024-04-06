using System.Net;

namespace ABMS_backend.DTO
{
    public class ResponseData<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrMsg { get; set; }
        public T? Data { get; set; }   
        public int? Count { get; set; }
    }
}
