using System.Web.Http;
using API.Common;

namespace API.Controllers
{
    public class HealthCheckController : ApiController
    {
        public Cache Cache { get; }

        public HealthCheckController(Cache cache)
        {
            Cache = cache;
        }

        [HttpGet]
        [Route("api/health")]
        public IHttpActionResult Get()
        {
            return Ok("Working");
        }
    }
}