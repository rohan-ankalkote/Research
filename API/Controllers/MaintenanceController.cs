using API.Common.Attributes;
using API.Common.Models;
using API.Repositories;
using System.Web.Http;

namespace API.Controllers
{
    public class MaintenanceController<TModel> : ApiController
        where TModel : ModelBase, new()
    {
        protected readonly IMaintenanceRepository<TModel> Repository;

        public MaintenanceController(IMaintenanceRepository<TModel> repository)
        {
            Repository = repository;
        }

        [HttpGet]
        [Entitlement(ActionCode = "SEARCH")]
        [Route("{key:int}")]
        public IHttpActionResult Get(int key)
        {
            var model = Repository.Get(key);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        [Entitlement(ActionCode = "INSERT")]
        [Route("")]
        public IHttpActionResult Insert(TModel model)
        {
            var insertedModel = Repository.Insert(model);

            return Ok(insertedModel);
        }

        [HttpPut]
        [Entitlement(ActionCode = "UPDATE")]
        [Route("")]
        public IHttpActionResult Update(TModel model)
        {
            var existingModel = Repository.Update(model);

            if (existingModel == null)
            {
                return BadRequest("Model not found");
            }

            return Ok(existingModel);
        }

        [HttpDelete]
        [Entitlement(ActionCode = "DELETE")]
        [Route("")]
        public IHttpActionResult Delete(int key)
        {
            var existingModel = Repository.Delete(key);

            return Ok(existingModel);
        }
    }
}