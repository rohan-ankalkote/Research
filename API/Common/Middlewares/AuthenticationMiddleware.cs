using System;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Common.Exceptions;
using Microsoft.Owin;
using Unity;

namespace API.Common.Middlewares
{
    public class AuthenticationMiddleware : OwinMiddleware
    {
        public AuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var principle = (ClaimsPrincipal)context.Request.User;

            if(!principle.Identity.IsAuthenticated)
            {
                throw new UnauthorizedException("Not Authorized.");
            }

            if(principle.Identity.Name == null)
            {
                throw new Exception("Name not found");
            }

            var cache = UnityConfig.Container.Resolve<Cache>();

            cache.UserName = principle.Identity.Name;

            await Next.Invoke(context);
        }
    }
}