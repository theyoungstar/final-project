using System;

namespace Catalyte.Apparel.Utilities.HttpResponseExceptions
{
    /// <summary>
    /// A custom exception for resource not found errors.
    /// </summary>
    [Serializable]
    public class UnprocessableEntity : Exception, IHttpResponseException
    {
        public UnprocessableEntity(string message)
        {
            Value = new(status: 422, error: "Purchase could not be completed the following products are not active {inactiveItemsList}.", message: message);
        }
        public HttpResponseExceptionValue Value { get; set; }
    }
}