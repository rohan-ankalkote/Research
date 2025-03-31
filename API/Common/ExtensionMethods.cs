using API.Common.Middlewares;
using Microsoft.Owin;
using Owin;
using Unity;

namespace API.Common
{
    public static class ExtensionMethods
    {
        public static IUnityContainer GetContainer(this IOwinContext context) => context.Get<IUnityContainer>("DiContainer");

        public static void SetContainer(this IOwinContext context, IUnityContainer container) => context.Set("DiContainer", container);

        public static void UseScoped<T>(this IAppBuilder appBuilder) where T : IScopedMiddleware
        {
            appBuilder.Use(async (context, next) =>
            {
                var middleware = context.GetContainer().Resolve<T>();
                await middleware.InvokeAsync(context, next);
            });
        }
    }
}