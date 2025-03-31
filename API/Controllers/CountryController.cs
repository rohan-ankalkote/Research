using API.Common.Attributes;
using API.Common.Models;
using API.Repositories;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/country")]
    [Entitlement(ComponentCode = "COUNTRY")]
    public class CountryController : MaintenanceController<CountryModel>
    {
        public CountryController(IMaintenanceRepository<CountryModel> repository) : base(repository)
        {
        }
    }
}