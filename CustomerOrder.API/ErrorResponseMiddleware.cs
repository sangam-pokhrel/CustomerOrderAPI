using CustomerOrder.Common.Constants;
using CustomerOrder.DTO;
using System.Net;
using System.Text.Json;

namespace CustomerOrder.API
{
    namespace EntertainmentHubPublicAPI.Middleware
    {
        public class ErrorResponseMiddleware
        {
            private RequestDelegate _next;

            public ErrorResponseMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (HttpRequestException ex)
                {
                    context.Response.ContentType = "application/json";

                    if (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse
                        {
                            Error = ErrorConstants.RecordNotFoundMsg,
                            ErrorMessage = ex.Message
                        }));
                    }

                    if (ex.StatusCode == HttpStatusCode.BadRequest)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse
                        {
                            Error = ErrorConstants.UnableToProcessReqMsg,
                            ErrorMessage = ex.Message
                        }));
                    }
                }
                catch (Exception ex)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse
                    {
                        Error = ErrorConstants.UnableToProcessReqMsg,
                        ErrorMessage = ex.Message
                    }));
                }
            }
        }
    }
}