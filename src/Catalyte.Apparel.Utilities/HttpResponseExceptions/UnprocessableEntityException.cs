using System;

namespace Catalyte.Apparel.Utilities.HttpResponseExceptions
{
    /// <summary>
    /// A custom exception for bad request errors.
    /// </summary>
    [Serializable]
    public class UnprocessableEntityException : Exception, IHttpResponseException
    {
        public UnprocessableEntityException(string message)
        {
            Value = new(status: 422, error: "Purchase could not be completed the following product(s) are not active", message: message);
        }
        public HttpResponseExceptionValue Value { get; set; }
    }
}
