using API.Common;
using API.Common.Middlewares;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Swashbuckle.Application;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Dispatcher;
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

            config.Services.Replace(typeof(IHttpControllerActivator), new ControllerActivator());

            config.EnableSwagger(s =>
            {
                s.SingleApiVersion("v1", "MyCode");
                s.ApiKey("Token").Description("Insert JWT Bearer Token").Name("Authorization").In("header");
            }).EnableSwaggerUi(c =>
            {
                c.EnableApiKeySupport("Authorization", "header");
            });

            config.MapHttpAttributeRoutes();

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Cache.SecretKey)),
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
                }
            });

            // Microsoft.IdentityModel.Protocols.OpenIdConnect
            //app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            //{
            //    AuthenticationMode = AuthenticationMode.Active,
            //    TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //        ValidateIssuer = true,
            //        ValidIssuer = "Valid Issuer",
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKeys =
            //            new ConfigurationManager<OpenIdConnectConfiguration>(
            //                    "https://host:port/.well-known/openid-configuration",
            //                    new OpenIdConnectConfigurationRetriever())
            //                .GetConfigurationAsync()
            //                .Result
            //                .JsonWebKeySet
            //                .GetSigningKeys()
            //    }
            //});

            app.MapWhen(context => Regex.IsMatch(context.Request.Uri.AbsolutePath.ToLower(), "/api"), appBuilder =>
            {
                appBuilder.Use<UnityDependencyScopeMiddleware>();
                appBuilder.UseScoped<UnhandledExceptionMiddleware>();
                appBuilder.UseScoped<AuthenticationMiddleware>();
                appBuilder.UseWebApi(config);
            });

            app.UseWebApi(config);
        }
    }
}
