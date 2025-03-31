using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace API.Common.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public bool AllowMultiple => false;

        public async Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var response = actionExecutedContext.Request.CreateErrorResponse(
                HttpStatusCode.InternalServerError,
                new HttpError(actionExecutedContext.Exception.Message));

            actionExecutedContext.Response = response;
        }
    }
}