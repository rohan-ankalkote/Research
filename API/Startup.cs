using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;
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

            app.UseWebApi(config);
        }
    }
}
