using API.Common.Exceptions;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.Common.Logging;

namespace API.Common.Middlewares
{
    /// <summary>
    /// Middleware to catch all unhandled exceptions and generate required response.
    /// </summary>
    public class UnhandledExceptionMiddleware : IScopedMiddleware
    {
        private Stream _originalStream;
        private IOwinContext _context;

        public async Task InvokeAsync(IOwinContext context, Func<Task> next)
        {
            try
            {
                _context = context;

                // Store the original body stream object, to override with error object if required.
                _originalStream = context.Response.Body;

                // Set new memory stream to body where response will be stored.
                context.Response.Body = new MemoryStream();

                await next();

                // Pointer will be pointing to end so make it point to start.
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                // If reached here, error has not occured, copy the response from body to original stream.
                await context.Response.Body.CopyToAsync(_originalStream);

                // Assign the original stream to body.
                context.Response.Body = _originalStream;
            }
            catch (UnauthorizedException ex)
            {
                await CreateErrorResponse(ex.Message, HttpStatusCode.Unauthorized);

                //Logger.WriteError(ex);
            }
            catch (Exception ex)
            {
                await CreateErrorResponse(ex.Message, HttpStatusCode.InternalServerError);

                //Logger.WriteError(ex);
            }
        }

        private async Task CreateErrorResponse(string message, HttpStatusCode statusCode)
        {
            var error = new HttpError(message);
            var content = new StringContent(JsonConvert.SerializeObject(error));
            var contentStream = await content.ReadAsStreamAsync();

            // If reached here, error has occured in middleware, copy the provided error model into the original stream.
            await contentStream.CopyToAsync(_originalStream);

            _context.Response.ContentType = "application/json";
            _context.Response.ContentLength = contentStream.Length;
            _context.Response.StatusCode = (int)statusCode;

            // Assign the original stream to body.
            _context.Response.Body = _originalStream;
        }
    }
}