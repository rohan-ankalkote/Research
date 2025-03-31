using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace API.Common.Filters
{
    /// <summary>
    /// This filter will be resolved for each http request.
    /// Dependencies can be injected through constructor.
    /// </summary>
    public interface IScopedFilter
    {
        Task InvokeAsync(HttpActionContext context);
    }
}