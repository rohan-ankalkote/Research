using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity;

namespace API.Common.Filters
{
    public class RootFilter : IActionFilter
    {
        private readonly List<Type> _filters;

        public RootFilter(Action<FilterBuilder> filterBuilderAction)
        {
            // Create the filter builder.
            var builder = new FilterBuilder();

            // Register all the scoped filters required.
            filterBuilderAction(builder);

            // Get all registered filters.
            _filters = builder.Build();
        }

        public bool AllowMultiple => false;

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            foreach(var filterType in _filters)
            {
                var filter = (IScopedFilter)actionContext.Request.GetOwinContext().GetContainer().Resolve(filterType);
                await filter.InvokeAsync(actionContext);
            }

            return await continuation();
        }
    }
}