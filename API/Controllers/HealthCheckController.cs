using System.Web.Http;
using API.Common;
using API.Common.Attributes;

namespace API.Controllers
{
    [RoutePrefix("api")]
    public class HealthCheckController : ApiController
    {
        public Cache Cache { get; }

        public HealthCheckController(Cache cache)
        {
            Cache = cache;
        }

        [HttpGet]
        [Route("health")]
        [Entitlement(OpenResource = true)]
        public IHttpActionResult Get()
        {
            return Ok($"Hello, {Cache.UserName}");
        }
    }
}