using API.Common.Exceptions;
using Microsoft.Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Common.Middlewares
{
    public class AuthenticationMiddleware : IScopedMiddleware
    {
        private readonly Cache _cache;

        public AuthenticationMiddleware(Cache cache)
        {
            _cache = cache;
        }

        public async Task InvokeAsync(IOwinContext context, Func<Task> next)
        {
            var principle = (ClaimsPrincipal)context.Request.User;

            if (!principle.Identity.IsAuthenticated)
            {
                throw new UnauthorizedException("Not Authorized.");
            }

            if (principle.Identity.Name == null)
            {
                throw new Exception("Name not found");
            }

            _cache.UserName = principle.Identity.Name;

            await next();
        }
    }
}