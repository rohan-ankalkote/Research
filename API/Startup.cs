using API.Common.Middlewares;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;
using System.Text.RegularExpressions;
using System.Web.Http;
using Unity.WebApi;

[assembly: OwinStartup(typeof(API.Startup))]

namespace API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            UnityConfig.RegisterComponents();

            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.Container);

            config.EnableSwagger(s =>
            {
                s.SingleApiVersion("v1", "MyCode");
            }).EnableSwaggerUi();

            config.MapHttpAttributeRoutes();

            app.MapWhen(context => Regex.IsMatch(context.Request.Uri.AbsolutePath.ToLower(), "/api"), appBuilder =>
            {
                appBuilder.Use<UnhandledExceptionMiddleware>();
                appBuilder.UseWebApi(config);
            });

            app.UseWebApi(config);
        }
    }
}
