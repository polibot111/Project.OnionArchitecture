using Serilog;
using System.Net;
using System.Text.Json;
using System.Text;
using FluentValidation;
using Application.Wrappers;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                string req = null;
                if (context.Request.Method != "GET")
                {
                    req = await GetRawBodyAsync(context.Request);
                }

                var messageId = Guid.NewGuid();
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>()
                {
                    Succeeded = false,
                    Message = $"A temporary error has occurred in the system! Please try again. (Error Code:{messageId})"
                };

                switch (error)
                {
                    case ValidationException e:
                        // custom application error
                        responseModel.Errors = e.Errors.Select(x => new Dictionary<string, string> { { x.PropertyName, x.ErrorMessage } }).ToList();
                        responseModel.Message = null;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;


                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;


                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var message = GetErrorMessage(error);
                var result = JsonSerializer.Serialize(responseModel);

                Log.Error("{@type} {@details} {@message} {@request} {@code} {@headers}", "globalerror", result, message, req, messageId.ToString(), context.Request.Headers.ToString());

                await response.WriteAsync(result);
            }
        }

        private async Task<string> GetRawBodyAsync(HttpRequest request, Encoding encoding = null)
        {
            request.Body.Seek(0, SeekOrigin.Begin);
            using var bufferReader = new StreamReader(request.Body);
            return await bufferReader.ReadToEndAsync();
        }

        private string GetErrorMessage(Exception ex)
        {
            var sb = new StringBuilder();
            sb.Append(ex.Message);
            sb.Append(ex.StackTrace);
            if (ex.InnerException != null)
                sb.Append(GetErrorMessage(ex.InnerException));
            return sb.ToString();
        }
    }
}
