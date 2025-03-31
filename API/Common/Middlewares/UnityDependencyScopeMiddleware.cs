using System.Threading.Tasks;
using Microsoft.Owin;

namespace API.Common.Middlewares
{
    /// <summary>
    /// Creates a new scope from which all dependencies in middleware, filter and controller will be resolved.
    /// </summary>
    public class UnityDependencyScopeMiddleware : OwinMiddleware
    {
        public UnityDependencyScopeMiddleware(OwinMiddleware next) : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            var childContainer = UnityConfig.Container.CreateChildContainer();
            
            context.SetContainer(childContainer);
            
            await Next.Invoke(context);
            
            childContainer.Dispose();;
        }
    }
}