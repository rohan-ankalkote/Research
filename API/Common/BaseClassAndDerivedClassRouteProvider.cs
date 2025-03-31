using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace API.Common
{
    public class BaseClassAndDerivedClassRouteProvider : DefaultDirectRouteProvider
    {
        public override IReadOnlyList<RouteEntry> GetDirectRoutes(
            HttpControllerDescriptor controllerDescriptor, 
            IReadOnlyList<HttpActionDescriptor> actionDescriptors,
            IInlineConstraintResolver constraintResolver)
        {
            if (controllerDescriptor.ControllerType.GetCustomAttribute<RoutePrefixAttribute>() == null) return new List<RouteEntry>();

            var existingRoutes = base.GetDirectRoutes(controllerDescriptor, actionDescriptors, constraintResolver);

            const string pattern = @"\{([a-zA-Z0-9_]+)(:([a-zA-Z0-9_]+))?\}";

            var newRoutes = new List<RouteEntry>();

            foreach (var actionDescriptor in actionDescriptors)
            {
                if (actionDescriptor is ReflectedHttpActionDescriptor reflectedActionDescriptor && reflectedActionDescriptor.MethodInfo.DeclaringType != controllerDescriptor.ControllerType)
                {
                    var routeTemplate =
                        controllerDescriptor.ControllerType.GetCustomAttribute<RoutePrefixAttribute>().Prefix + "/" + reflectedActionDescriptor.MethodInfo.GetCustomAttribute<RouteAttribute>().Template;

                    var constraintDictionary = new HttpRouteValueDictionary();
                    var matches = new Regex(pattern).Matches(routeTemplate);

                    foreach (Match match in matches)
                    {
                        var parameterName = match.Groups[1].Value;
                        var constraintName = match.Groups[3].Value;

                        if (!string.IsNullOrEmpty(constraintName))
                        {
                            var constraint = constraintResolver.ResolveConstraint(constraintName);
                            if (constraint != null)
                            {
                                constraintDictionary.Add(parameterName, constraint);
                            }
                        }
                    }

                    var dataTokenDictionary = new HttpRouteValueDictionary()
                    {
                        { "actions", new [] { actionDescriptor } },
                        { "precedence", 1 }
                    };

                    routeTemplate = Regex.Replace(routeTemplate, pattern, "{$1}");

                    var route = new RouteEntry(
                        null,
                        new HttpRoute(
                            routeTemplate,
                            new HttpRouteValueDictionary(),
                            constraintDictionary,
                            dataTokenDictionary));

                    newRoutes.Add(route);
                }
            }

            newRoutes.AddRange(existingRoutes);

            return newRoutes;
        }
    }
}