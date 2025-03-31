using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace API.Common.Middlewares
{
    public interface IScopedMiddleware
    {
        Task InvokeAsync(IOwinContext context, Func<Task> next);
    }
}