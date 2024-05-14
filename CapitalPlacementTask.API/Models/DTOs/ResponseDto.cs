using System.Net;

namespace Exercise.Models.DTOs
{
    public class ResponseDto<T>
    {
        public HttpStatusCode Status { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }

        public ResponseDto(HttpStatusCode status, string errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public ResponseDto(HttpStatusCode status)
        {
            Status = status;
        }

        public ResponseDto(dynamic data)
        {
            Data = data;
            Status = HttpStatusCode.OK;
        }
    }
}
