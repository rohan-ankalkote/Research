using API.Common.Attributes;
using API.Common.MediaTypeFormatters;
using API.Common.Models;
using API.Repositories;
using System;
using System.IO;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/user")]
    [Entitlement(ComponentCode = "USER")]
    public class UserController : MaintenanceController<UserModel>
    {
        public UserController(IMaintenanceRepository<UserModel> repository) : base(repository)
        {
        }

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
    }
}