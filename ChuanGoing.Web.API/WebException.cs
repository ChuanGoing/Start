using Microsoft.AspNetCore.Http;
using System.Net;

namespace ChuanGoing.Base.Exceptions
{
    public class WebException: InnerException
    {
        public HttpStatusCode HttpStatus { get; set; }

        public HttpRequest Request { get; private set; }

        public WebException(HttpStatusCode httpStatus, int errorCode, string message)
             : base(errorCode, message)
        {
            HttpStatus = httpStatus;
        }

        public WebException(HttpStatusCode httpStatus, int errorCode, string message, HttpRequest request)
            : this(httpStatus, errorCode, message)
        {
            Request = request;
        }

        public WebException(int errorCode, string message)
            : base(errorCode, message)
        {
            HttpStatus = HttpStatusCode.BadRequest;
        }
    }
}
