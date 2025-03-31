using API.Common.Attributes;
using API.Common.Exceptions;
using API.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace API.Common.Filters
{
    public class EntitlementFilter : IScopedFilter
    {
        public async Task InvokeAsync(HttpActionContext context)
        {
            var attributeOnController = 
                context.ControllerContext.Controller.GetType().GetCustomAttributes<EntitlementAttribute>().FirstOrDefault();

            var attributeOnMethod =
                context.ActionDescriptor.GetCustomAttributes<EntitlementAttribute>().FirstOrDefault();

            if (attributeOnMethod != null && attributeOnMethod.OpenResource) return;

            if (attributeOnController == null || attributeOnMethod == null)
            {
                throw new UnauthorizedException("User doesn't have entitlements.");
            }

            var existingEntitlement = new EntitlementModel()
            {
                ComponentCode = "COUNTRY",
                ActionCodes = new List<string>() { "SEARCH", "INSERT" }
            };

            if (attributeOnController.ComponentCode != existingEntitlement.ComponentCode || existingEntitlement.ActionCodes.All(e => e != attributeOnMethod.ActionCode))
            {
                throw new UnauthorizedException("User doesn't have entitlements.");
            }
        }
    }
}