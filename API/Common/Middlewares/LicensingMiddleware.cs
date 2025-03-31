using System;
using System.Threading.Tasks;
using API.Common.Exceptions;
using Microsoft.Owin;

namespace API.Common.Middlewares
{
    public class LicensingMiddleware : IScopedMiddleware
    {
        private readonly Cache _cache;

        public LicensingMiddleware(Cache cache)
        {
            _cache = cache;
        }

        public async Task InvokeAsync(IOwinContext context, Func<Task> next)
        {
            var isLicenced = _cache.UserName == "rohan";

            if(!isLicenced)
            {
                throw new UnauthorizedException("Not Licenced");
            }

            await next();
        }
    }
}