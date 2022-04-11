using System;
using System.Collections.Generic;

namespace Catalyte.Apparel.Utilities.HttpResponseExceptions
{
    public class HttpResponseExceptionValue
    {
        public HttpResponseExceptionValue()
        {
            Timestamp = DateTime.UtcNow;
        }
        public HttpResponseExceptionValue(int status, string error, string message)
        {
            Timestamp = DateTime.UtcNow;
            Status = status;
            Error = error;
            ErrorMessage = message;
            
        }
        public HttpResponseExceptionValue(int status, string error, List<string> messages)
        {
            Timestamp = DateTime.UtcNow;
            Status = status;
            Error = error;
            ErrorMessages = messages;
        }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public string Error { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string ErrorMessage { get; set; }
        

    }
}
