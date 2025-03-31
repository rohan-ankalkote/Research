using API.Common;
using API.Common.Attributes;
using API.Common.Models;
using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/country")]
    [Entitlement(ComponentCode = "COUNTRY")]
    public class CountryController : ApiController
    {
        [HttpGet]
        [Entitlement(ActionCode = "SEARCH")]
        [Route("{key:int}")]
        public IHttpActionResult Get(int key)
        {
            var model = Repository.Countries.FirstOrDefault(c => c.Id == key);

            if(model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        [Entitlement(ActionCode = "INSERT")]
        [Route("")]
        public IHttpActionResult Insert(CountryModel model)
        {
            var id = Repository.Countries.Count == 0 ? 1 : Repository.Countries.Select(c => c.Id).Max() + 1;
            var insertedModel = new CountryModel()
            {
                Id = id,
                Name = model.Name,
                Code = model.Code
            };
            Repository.Countries.Add(insertedModel);

            return Ok(insertedModel);
        }

        [HttpPut]
        [Entitlement(ActionCode = "UPDATE")]
        [Route("")]
        public IHttpActionResult Update(CountryModel model)
        {
            var existingModel = Repository.Countries.FirstOrDefault(c => c.Id == model.Id);

            if(existingModel == null)
            {
                return BadRequest("Model not found");
            }

            existingModel.Name = model.Name;
            existingModel.Code = model.Code;

            return Ok(existingModel);
        }

        [HttpDelete]
        [Entitlement(ActionCode = "DELETE")]
        [Route("")]
        public IHttpActionResult Delete(int key)
        {
            var existingModel = Repository.Countries.FirstOrDefault(c => c.Id == key);

            if (existingModel == null)
            {
                return BadRequest("Model not found");
            }

            Repository.Countries.Remove(existingModel);

            return Ok(existingModel);
        }
    }
}