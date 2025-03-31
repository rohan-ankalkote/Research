using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using API.Common;
using API.Common.Attributes;
using API.Common.MediaTypeFormatters;
using API.Common.Models;

namespace API.Controllers
{
    [RoutePrefix("api/user")]
    [Entitlement(ComponentCode = "USER")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("upload")]
        [Entitlement(ActionCode = "UPLOAD")]
        public IHttpActionResult UploadProfileImage(IFormFile file)
        {
            if (file == null) throw new Exception("File not provided.");

            byte[] byteArray;
            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                byteArray = binaryReader.ReadBytes(file.Length);
            }

            return Ok("Uploaded");
        }

        [HttpGet]
        [Entitlement(ActionCode = "SEARCH")]
        [Route("{key:int}")]
        public IHttpActionResult Get(int key)
        {
            var model = Repository.Users.FirstOrDefault(c => c.Id == key);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        [Entitlement(ActionCode = "INSERT")]
        [Route("")]
        public IHttpActionResult Insert(UserModel model)
        {
            var id = Repository.Users.Count == 0 ? 1 : Repository.Users.Select(c => c.Id).Max() + 1;
            var insertedModel = new UserModel()
            {
                Id = id,
                Name = model.Name
            };
            Repository.Users.Add(insertedModel);

            return Ok(insertedModel);
        }

        [HttpPut]
        [Entitlement(ActionCode = "UPDATE")]
        [Route("")]
        public IHttpActionResult Update(UserModel model)
        {
            var existingModel = Repository.Users.FirstOrDefault(c => c.Id == model.Id);

            if (existingModel == null)
            {
                return BadRequest("Model not found");
            }

            existingModel.Name = model.Name;

            return Ok(existingModel);
        }

        [HttpDelete]
        [Entitlement(ActionCode = "DELETE")]
        [Route("")]
        public IHttpActionResult Delete(int key)
        {
            var existingModel = Repository.Users.FirstOrDefault(c => c.Id == key);

            if (existingModel == null)
            {
                return BadRequest("Model not found");
            }

            Repository.Users.Remove(existingModel);

            return Ok(existingModel);
        }
    }
}