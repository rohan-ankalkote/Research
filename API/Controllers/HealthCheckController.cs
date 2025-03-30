using System.Web.Http;

namespace API.Controllers
{
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        [Route("api/health")]
        public IHttpActionResult Get()
        {
            return Ok("Working");
        }
    }
}